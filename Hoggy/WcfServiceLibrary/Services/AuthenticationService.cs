using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Contracts;
using DataTransferObjects;
using DataAccessLayer.Entities;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AuthenticationService : IAuthenticationContract
    {
        IRepository _repository;

        public AuthenticationService(IRepository repository)
        {
            _repository = repository;
        }

        public bool CheckPasswordIsCorrect(UserDTO user)
        {
            UserEntity u = _repository.GetItem<UserEntity>(x => x.Email == user.Email);
            if (u == null)
                return false;

            return u.Password == user.Password;
        }

        public bool CheckUserIsRegistered(UserDTO user)
        {
            return _repository.GetItem<UserEntity>(x => x.Email == user.Email) == null;
        }

        public AuthenticationToken Login(UserDTO _user)
        {
            UserEntity user = _repository.GetItem<UserEntity>(x => x.Email == _user.Email);
            if (_user.Password == user.Password)
            {
                AuthenticationToken token = new AuthenticationToken();
                token.CreationDate = DateTime.Now;
                token.ExpireDate = DateTime.Now.AddDays(1);
                AuthenticationTokenEntity tokenEntity = new AuthenticationTokenEntity();
                AutoMapper.Mapper.Map(token, tokenEntity);
                _repository.Add<AuthenticationTokenEntity>(tokenEntity);
                _repository.Save();
                return token;
            }
            else
                return null;
        }
    }
}
