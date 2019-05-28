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
using System.Security.Cryptography;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AuthenticationService : IAuthenticationContract
    {
        private readonly IRepository _repository;

        public AuthenticationService(IRepository repository)
        {
            _repository = repository;
        }

        public bool CheckPasswordIsCorrect(string email, string password)
        {
            UserEntity u = _repository.GetItem<UserEntity>(x => x.Email == email);

            if (u == null)
                return false;
            else
                return u.Password == password;
        }

        public bool CheckUserIsRegistered(string email)
        {
            return _repository.GetItem<UserEntity>(x => x.Email == email) == null;
        }

        public AuthenticationToken Login(string email, string password)
        {
            UserEntity user = _repository.GetItem<UserEntity>(x => x.Email == email);

            if (user == null)
                return null;

            if(password == user.Password)
            {
                AuthenticationTokenEntity oldToken = _repository.GetItem<AuthenticationTokenEntity>(x => x.User == user);
                if (oldToken != null)
                    _repository.Delete(oldToken);
                AuthenticationTokenEntity tokenEntity = new AuthenticationTokenEntity();
                tokenEntity.User = user;
                SHA256Managed cryptor = new SHA256Managed();

                tokenEntity.Value = Encoding.Unicode.GetString(
                    cryptor.ComputeHash(
                        Encoding.Unicode.GetBytes(email + "hoggySaLt" + DateTime.Now.ToString() + password)));
                _repository.Add(tokenEntity);
                _repository.Save();

                AuthenticationToken token = new AuthenticationToken();
                token.Value = tokenEntity.Value.Clone() as string;
                return token;
            }
            else
                return null;
        }
    }
}
