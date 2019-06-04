using DataTransferObjects;
using System.ServiceModel;

namespace WcfServiceLibrary.Contracts
{
    public interface INotifiactionCallbackContract
    {
        [OperationContract]
        void OnBoardAdded(BoardDTO board);
        [OperationContract]
        void OnParticipantAdded(UserDTO user, int boardId);
        [OperationContract]
        void OnColumnAdded(ColumnDTO column, int boardId);
        [OperationContract]
        void OnBoardTagAdded(TagDTO tag, int boardId);
        [OperationContract]
        void OnCardTagAdded(int tagId, int cardId);
        [OperationContract]
        void OnHistoryEventAdded(HistoryEventDTO historyEvent, int boardId);
        [OperationContract]
        void OnCardAdded(CardDTO card, int columnId);
        [OperationContract]
        void OnCardSubscribersAdded(UserDTO user, int cardId);
        [OperationContract]
        void OnCardCommentAdded(CommentDTO comment, int cardId);
    }
}
