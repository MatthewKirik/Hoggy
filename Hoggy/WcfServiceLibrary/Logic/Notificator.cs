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
        private static object _subscribersLocker = new object();
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
            Task.Run(() =>
            {
                foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            s.Callback.OnBoardAdded(board);
                        }
                        catch (Exception)
                        {
                            lock (_subscribersLocker)
                            {
                                Subscribers.Remove(s);
                            }
                        }
                    });
                }
            });
        }

        public void OnBoardTagAdded(TagDTO tag, int boardId)
        {
            Task.Run(() =>
            {
                foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            s.Callback.OnBoardTagAdded(tag, boardId);
                        }
                        catch (Exception)
                        {
                            lock (_subscribersLocker)
                            {
                                Subscribers.Remove(s);
                            }
                        }
                    });
                }
            });
        }

        public void OnCardAdded(CardDTO card, int columnId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnCardAdded(card, columnId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnCardCommentAdded(CommentDTO comment, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnCardCommentAdded(comment, cardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnCardSubscribersAdded(UserDTO user, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnCardSubscribersAdded(user, cardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnCardTagAdded(int tagId, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnCardTagAdded(tagId, cardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnColumnAdded(ColumnDTO column, int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnColumnAdded(column, boardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnHistoryEventAdded(HistoryEventDTO historyEvent, int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnHistoryEventAdded(historyEvent, boardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnParticipantAdded(UserDTO user, int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        Console.WriteLine("Participant added");
                        s.Callback.OnParticipantAdded(user, boardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnBoardDeleted(int boardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnBoardDeleted(boardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnColumnDeleted(int boardId, int columnId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnColumnDeleted(boardId, columnId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnBoardTagDeleted(int boardId, int tagId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnBoardTagDeleted(boardId, tagId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnCardDeleted(int boardId, int columnId, int cardId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnCardDeleted(boardId, columnId, cardId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnCardTagDeleted(int boardId, int cardId, int tagId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnCardTagDeleted(boardId, cardId, tagId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }

        public void OnCardMoved(int cardId, int originalColumnId, int destinationColumnId)
        {
            foreach (var s in Subscribers.Where(x => x.SecurityGroupId == TargetSecurityGroup))
            {
                Task.Run(() =>
                {
                    try
                    {
                        s.Callback.OnCardMoved(cardId, originalColumnId, destinationColumnId);
                    }
                    catch (Exception)
                    {
                        lock (_subscribersLocker)
                        {
                            Subscribers.Remove(s);
                        }
                    }
                });
            }
        }
    }
}
