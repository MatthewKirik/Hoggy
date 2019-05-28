using DataTransferObjects;
using System.ServiceModel;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IRegistrationContract
    {
        [OperationContract]
        bool RegisterUser(UserDTO user, string password);
    }
}
