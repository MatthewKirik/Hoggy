
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RegistrationService : IRegistrationContract
    {
        IRepository _repository;

        public RegistrationService(IRepository repository)
        {
            _repository = repository;
        }

        public bool RegisterUser(UserDTO user, string password)
        {
            if (_repository.GetItem<UserEntity>(x => x.Email == user.Email || x.Login == user.Login) == null)
            {

                UserEntity newUser = new UserEntity();
                newUser.Email = user.Email;
                newUser.Login = user.Login;
                newUser.Password = password;
                _repository.Add(newUser);
                _repository.Save();


                UserProfileEntity profileEntity = new UserProfileEntity();
                profileEntity.User = newUser;
                _repository.Add(profileEntity);
                _repository.Save();

                newUser.Profile = profileEntity;
                _repository.Update(newUser);
                _repository.Save();
                return true;
            }
            else
                return false;
        }
    }
}
