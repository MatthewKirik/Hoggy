using DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IDataExchangeContract
    {
        [OperationContract]
        UserDTO GetUser(AuthenticationToken token);
        [OperationContract]
        List<BoardDTO> GetBoards(AuthenticationToken token, int UserId);
        [OperationContract]
        List<BoardDTO> GetParticipatedBoards(AuthenticationToken token, int UserId);
        [OperationContract]
        List<UserDTO> GetParticipants(AuthenticationToken token, int BoardId);
        [OperationContract]
        List<ColumnDTO> GetColumns(AuthenticationToken token, int BoardId);
        [OperationContract]
        List<HistoryEventDTO> GetHistoryEvents(AuthenticationToken token, int BoardId);
        [OperationContract]
        List<CardDTO> GetCards(AuthenticationToken token, int ColumnId);
        [OperationContract]
        List<UserDTO> GetCardSubscribers(AuthenticationToken token, int CardId);
        [OperationContract]
        List<CommentDTO> GetCardComments(AuthenticationToken token, int CardId);
    }
}
