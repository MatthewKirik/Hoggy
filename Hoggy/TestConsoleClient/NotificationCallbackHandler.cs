using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using TestConsoleClient.NotificationService;

namespace TestConsoleClient
{
    public class NotificationCallbackHandler : INotificationContractCallback
    {
        public void OnBoardAdded(BoardDTO board)
        {
            Console.WriteLine(board.Name + " added");
        }

        public void OnBoardDeleted(int boardId)
        {
            throw new NotImplementedException();
        }

        public void OnBoardTagAdded(TagDTO tag, int boardId)
        {
            Console.WriteLine(tag.Name + " added");
        }

        public void OnBoardTagDeleted(int boardId, int tagId)
        {
            throw new NotImplementedException();
        }

        public void OnCardAdded(CardDTO card, int columnId)
        {

        }

        public void OnCardCommentAdded(CommentDTO comment, int cardId)
        {

        }

        public void OnCardDeleted(int boardId, int columnId, int cardId)
        {
            throw new NotImplementedException();
        }

        public void OnCardSubscribersAdded(UserDTO user, int cardId)
        {

        }

        public void OnCardTagAdded(int tagId, int cardId)
        {

        }

        public void OnCardTagDeleted(int boardId, int cardId, int tagId)
        {
            throw new NotImplementedException();
        }

        public void OnColumnAdded(ColumnDTO column, int boardId)
        {
            Console.WriteLine(column.Name + " added");
        }

        public void OnColumnDeleted(int boardId, int columnId)
        {
            throw new NotImplementedException();
        }

        public void OnHistoryEventAdded(HistoryEventDTO historyEvent, int boardId)
        {

        }

        public void OnParticipantAdded(UserDTO user, int boardId)
        {

        }

        public void OnParticipatedBoardAdded(BoardDTO board)
        {

        }
    }
}
