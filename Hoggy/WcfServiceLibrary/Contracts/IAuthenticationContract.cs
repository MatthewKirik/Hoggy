using System.ServiceModel;
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