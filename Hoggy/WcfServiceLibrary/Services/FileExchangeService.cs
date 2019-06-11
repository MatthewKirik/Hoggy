using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataTransferObjects;
using DataTransferObjects.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Contracts;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FileExchangeService : IFileExchangeContract
    {
        private readonly IRepository _repository;
        private readonly IFileRepository _fileRepository;
        public FileExchangeService(IRepository repository, IFileRepository fileRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
        }

        public GetImageResponseMessage GetUserProfileImage(GetImageRequestMessage request)
        {
            try
            {
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == request.Token.Value).User;
                if (user == null)
                    return null;
                UserProfileEntity profile = user.Profile;
                MemoryStream memoryStream = new MemoryStream();
                byte[] file = _fileRepository.GetFile(profile.Image);
                memoryStream.Write(file, 0, file.Length);
                memoryStream.Flush();
                memoryStream.Position = 0;
                GetImageResponseMessage response = new GetImageResponseMessage()
                {
                    FileByteStream = memoryStream,
                    Length = memoryStream.Length
                };
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetUserProfileImage(AddImageRequestMessage request)
        {
            try
            {
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == request.Token.Value).User;
                if (user == null)
                    return;
                UserProfileEntity profile = user.Profile;

                MemoryStream stream = new MemoryStream();
                byte[] buffer = new byte[2048];
                int bytesRead = 0;
                do
                {
                    bytesRead = request.FileByteStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, bytesRead);
                } while (stream.Length < request.Length);


                byte[] file = new byte[stream.Length];
                stream.Flush();
                stream.Position = 0;
                stream.Read(file, 0, file.Length);
                string key = _fileRepository.AddFile(file);
                profile.Image = key;
                _repository.Update(profile);
                _repository.Save();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
