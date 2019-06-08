using System.ServiceModel;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RegistrationService : IRegistrationContract
    {
        private readonly IRepository _repository;

        public RegistrationService(IRepository repository)
        {
            _repository = repository;
        }

        public bool RegisterUser(UserDTO user, string password)
        {
            try
            {
                if (_repository.GetItem<UserEntity>(x => x.Email == user.Email || x.Login == user.Login) == null)
                {

                    UserProfileEntity profileEntity = new UserProfileEntity();

                    UserEntity newUser = new UserEntity();
                    newUser.Email = user.Email;
                    newUser.Login = user.Login;
                    newUser.Password = password;
                    newUser.Profile = profileEntity;
                    profileEntity.User = newUser;
                    _repository.Add(newUser);
                    _repository.Add(profileEntity);
                    _repository.Save();
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
