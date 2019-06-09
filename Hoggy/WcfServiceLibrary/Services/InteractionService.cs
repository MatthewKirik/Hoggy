using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using System.ServiceModel;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Interfaces;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class InteractionService : IInteractionContract
    {
        private readonly IRepository _repository;
        private readonly INotificator _notificator;
        public InteractionService(IRepository repository, INotificator notificator)
        {
            _repository = repository;
            _notificator = notificator;
        }

        public bool MoveCard(AuthenticationToken token, int cardId, int destinationColumnId)
        {
            try
            {
                CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == cardId);
                if (!Validator.HasAccess(_repository, token, card))
                    return false;
                ColumnEntity column = _repository.GetItem<ColumnEntity>(x => x.Id == destinationColumnId);
                if (column == null)
                    return false;
                int origId = card.Column.Id;
                card.Column = column;
                _repository.Update(card);
                _repository.Save();
                _notificator.WithSecurityGroup(card.SecurityGroupId).OnCardMoved(cardId, origId, destinationColumnId);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
