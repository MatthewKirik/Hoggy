using AutoMapper;
using DataTransferObjects;
using PresentationLayer.Models;
using PresentationLayer.NotificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Helpers
{
    public class CallbackHandler : INotificationContractCallback
    {
        //HANDLERS
        static Action<CardModel, int> _actAddCard;
        static Action<int, int, int> _actMoveCard;

        public void AddNewCardHandler(Action<CardModel, int> actAddCard)
        {
            _actAddCard = actAddCard;
        }

        public void AddMoveCardHandler(Action<int, int, int> actMoveCard)
        {
            _actMoveCard = actMoveCard;
        }

        public void OnBoardAdded(BoardDTO board)
        {
            
        }

        public void OnBoardDeleted(int boardId)
        {
            
        }

        public void OnBoardTagAdded(TagDTO tag, int boardId)
        {
           
        }

        public void OnBoardTagDeleted(int boardId, int tagId)
        {
            
        }

        public void OnCardAdded(CardDTO card, int columnId)
        {
            _actAddCard(Mapper.Map<CardModel>(card), columnId);
        }

        public void OnCardCommentAdded(CommentDTO comment, int cardId)
        {
            
        }

        public void OnCardDeleted(int boardId, int columnId, int cardId)
        {
            
        }

        public void OnCardMoved(int cardId, int originalColumnId, int destinationColumnId)
        {
            
        }

        public void OnCardSubscribersAdded(UserDTO user, int cardId)
        {
            
        }

        public void OnCardTagAdded(int tagId, int cardId)
        {
            
        }

        public void OnCardTagDeleted(int boardId, int cardId, int tagId)
        {
            
        }

        public void OnColumnAdded(ColumnDTO column, int boardId)
        {
            
        }

        public void OnColumnDeleted(int boardId, int columnId)
        {
            
        }

        public void OnHistoryEventAdded(HistoryEventDTO historyEvent, int boardId)
        {
            
        }

        public void OnParticipantAdded(UserDTO user, int boardId)
        {
            
        }
    }
}
