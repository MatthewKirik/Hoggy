using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Interfaces;
using WcfServiceLibrary.Models;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class NotificationService : INotificationContract
    {
        private readonly IRepository _repository;
        private readonly INotificator _notificator;

        public NotificationService(IRepository repository, INotificator notificator)
        {
            _repository = repository;
            _notificator = notificator;
        }

        public bool Subscribe(AuthenticationToken token, int boardId)
        {
            try
            {
                BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == boardId);
                if (!Validator.HasAccess(_repository, token, board))
                    return false;
                string email = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User.Email;

                SubscriberModel toAdd = new SubscriberModel()
                {
                    Callback = OperationContext.Current.GetCallbackChannel<INotifiactionCallbackContract>(),
                    SecurityGroupId = board.SecurityGroupId,
                    Email = email
                };
                _notificator.Subscribers.Add(toAdd);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UnSubscribe(AuthenticationToken token, int boardId)
        {
            try
            {
                BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == boardId);
                if (!Validator.HasAccess(_repository, token, board))
                    return false;

                SubscriberModel sub = _notificator.Subscribers.FirstOrDefault(x => x.SecurityGroupId == board.SecurityGroupId);
                if (sub == null)
                    return false;
                _notificator.Subscribers.Remove(sub);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
