using DataTransferObjects;
using PresentationLayer.NotificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Helpers
{
    public class CallbackHandler : INotificationContractCallback
    {
        public void OnBoardAdded(BoardDTO board)
        {
            throw new NotImplementedException();
        }

        public void OnBoardTagAdded(TagDTO tag, int boardId)
        {
            throw new NotImplementedException();
        }

        public void OnCardAdded(CardDTO card, int columnId)
        {
            throw new NotImplementedException();
        }

        public void OnCardCommentAdded(CommentDTO comment, int cardId)
        {
            throw new NotImplementedException();
        }

        public void OnCardSubscribersAdded(UserDTO user, int cardId)
        {
            throw new NotImplementedException();
        }

        public void OnCardTagAdded(int tagId, int cardId)
        {
            throw new NotImplementedException();
        }

        public void OnColumnAdded(ColumnDTO column, int boardId)
        {
            throw new NotImplementedException();
        }

        public void OnHistoryEventAdded(HistoryEventDTO historyEvent, int boardId)
        {
            throw new NotImplementedException();
        }

        public void OnParticipantAdded(UserDTO user, int boardId)
        {
            throw new NotImplementedException();
        }
    }
}
