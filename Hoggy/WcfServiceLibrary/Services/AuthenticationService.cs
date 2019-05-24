using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Contracts;
using DataTransferObjects;

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

        public bool CheckUserIsRegistered(UserDTO user)
        {
            return false;
        }
    }
}
