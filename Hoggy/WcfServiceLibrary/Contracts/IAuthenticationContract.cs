using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IAuthenticationContract
    {
        [OperationContract]
        bool CheckUserIsRegistered(string email);
        [OperationContract]
        bool CheckPasswordIsCorrect(string email, string password);
        [OperationContract]
        AuthenticationToken Login(string email, string password);
    }
}