using DataTransferObjects;
using System.ServiceModel;

namespace WcfServiceLibrary.Contracts
{
    public interface INotifiactionCallbackContract
    {
        [OperationContract(IsOneWay = true)]
        void OnBoardAdded(BoardDTO board);
        [OperationContract(IsOneWay = true)]
        void OnParticipantAdded(UserDTO user, int boardId);
        [OperationContract(IsOneWay = true)]
        void OnColumnAdded(ColumnDTO column, int boardId);
        [OperationContract(IsOneWay = true)]
        void OnBoardTagAdded(TagDTO tag, int boardId);
        [OperationContract(IsOneWay = true)]
        void OnCardTagAdded(int tagId, int cardId);
        [OperationContract(IsOneWay = true)]
        void OnHistoryEventAdded(HistoryEventDTO historyEvent, int boardId);
        [OperationContract(IsOneWay = true)]
        void OnCardAdded(CardDTO card, int columnId);
        [OperationContract(IsOneWay = true)]
        void OnCardSubscribersAdded(UserDTO user, int cardId);
        [OperationContract(IsOneWay = true)]
        void OnCardCommentAdded(CommentDTO comment, int cardId);

        [OperationContract(IsOneWay = true)]
        void OnBoardDeleted(int boardId);
        [OperationContract(IsOneWay = true)]
        void OnColumnDeleted(int boardId, int columnId);
        [OperationContract(IsOneWay = true)]
        void OnBoardTagDeleted(int boardId, int tagId);
        [OperationContract(IsOneWay = true)]
        void OnCardDeleted(int boardId, int columnId, int cardId);
        [OperationContract(IsOneWay = true)]
        void OnCardTagDeleted(int boardId, int cardId, int tagId);

        [OperationContract(IsOneWay = true)]
        void OnCardEdited(CardDTO card);
        [OperationContract(IsOneWay = true)]
        void OnColumnEdited(ColumnDTO column);

        [OperationContract(IsOneWay = true)]
        void OnCardMoved(int cardId, int originalColumnId, int destinationColumnId);

        [OperationContract]
        void OnIncomeInvitation(InvitationDTO invitation, string recepientEmail);
    }
}
