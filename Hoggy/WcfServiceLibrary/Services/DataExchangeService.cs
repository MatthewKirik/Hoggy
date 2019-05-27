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
    public class DataExchangeService : IDataExchangeContract
    {
        IRepository _repository;

        public DataExchangeService(IRepository repository)
        {
            _repository = repository;
        }

        public List<BoardDTO> GetBoards(AuthenticationToken token, int UserId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;

            if (user == null)
                return null;

            if (user.Id == UserId)
            {
                List<BoardDTO> boards = new List<BoardDTO>();
                foreach(var b in user.Profile.Boards)
                {
                    BoardDTO boardDTO = new BoardDTO();
                    boardDTO.CreationDate = b.CreationDate;
                    boardDTO.Description = b.Description;
                    boardDTO.Name = b.Name;
                    boardDTO.Id = b.Id;
                    boards.Add(boardDTO);
                }
                return boards;
            }
            else
                return null;
        }
        
    }
}
