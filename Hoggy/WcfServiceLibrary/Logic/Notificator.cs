using DataAccessLayer.Entities;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Interfaces;
using WcfServiceLibrary.Models;

namespace WcfServiceLibrary.Logic
{
    public class Notificator : INotificator
    {
        private List<SubscriberModel> _subscribers;
        public List<SubscriberModel> Subscribers { get => _subscribers ?? (_subscribers = new List<SubscriberModel>()); }
        int TargetSecurityGroup { get; set; }

        public INotificator WithSecurityGroup(int id)
        {
            TargetSecurityGroup = id;
            return this;
        }

        public void OnBoardAdded(BoardDTO board)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnBoardAdded(board);
            }
        }

        public void OnBoardTagAdded(TagDTO tag, int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnBoardTagAdded(tag, boardId);
            }
        }

        public void OnCardAdded(CardDTO card, int columnId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnCardAdded(card, columnId);
            }
        }

        public void OnCardCommentAdded(CommentDTO comment, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnCardCommentAdded(comment, cardId);
            }
        }

        public void OnCardSubscribersAdded(UserDTO user, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnCardSubscribersAdded(user, cardId);
            }
        }

        public void OnCardTagAdded(int tagId, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnCardTagAdded(tagId, cardId);
            }
        }

        public void OnColumnAdded(ColumnDTO column, int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnColumnAdded(column, boardId);
            }
        }

        public void OnHistoryEventAdded(HistoryEventDTO historyEvent, int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnHistoryEventAdded(historyEvent, boardId);
            }
        }

        public void OnParticipantAdded(UserDTO user, int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnParticipantAdded(user, boardId);
            }
        }

        public void OnBoardDeleted(int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnBoardDeleted(boardId);
            }
        }

        public void OnColumnDeleted(int boardId, int columnId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnColumnDeleted(boardId, columnId);
            }
        }

        public void OnBoardTagDeleted(int boardId, int tagId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnBoardTagDeleted(boardId, tagId);
            }
        }

        public void OnCardDeleted(int boardId, int columnId, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnCardDeleted(boardId, columnId, cardId);
            }
        }

        public void OnCardTagDeleted(int boardId, int cardId, int tagId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                s.Callback.OnCardTagDeleted(boardId, cardId, tagId);
            }
        }
    }
}
