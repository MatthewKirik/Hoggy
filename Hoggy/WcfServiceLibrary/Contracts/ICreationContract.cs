using DataTransferObjects;
using System.ServiceModel;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface ICreationContract
    {
        [OperationContract]
        bool AddBoard(AuthenticationToken token, BoardDTO board);
        [OperationContract]
        bool AddColumn(AuthenticationToken token, ColumnDTO column, int boardId);
        [OperationContract]
        bool AddCard(AuthenticationToken token, CardDTO card, int columnId);
    }
}
