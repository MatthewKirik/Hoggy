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

        public void OnBoardTagAdded(TagDTO tag, int boardId)
        {
            Console.WriteLine(tag.Name + " added");
        }

        public void OnCardAdded(CardDTO card, int columnId)
        {

        }

        public void OnCardCommentAdded(CommentDTO comment, int cardId)
        {

        }

        public void OnCardSubscribersAdded(UserDTO user, int cardId)
        {

        }

        public void OnCardTagAdded(int tagId, int cardId)
        {

        }

        public void OnColumnAdded(ColumnDTO column, int boardId)
        {
            Console.WriteLine(column.Name + " added");
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
