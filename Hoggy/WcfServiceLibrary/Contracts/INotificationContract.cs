using DataTransferObjects;
using System.ServiceModel;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract(CallbackContract = typeof(INotifiactionCallbackContract))]
    public interface INotificationContract
    {
        [OperationContract]
        bool Subscribe(AuthenticationToken token, int boardId);
    }
}
