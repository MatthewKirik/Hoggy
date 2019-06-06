using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Interfaces;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EditionService : IEditionContract
    {
        private readonly IRepository _repository;
        private readonly INotificator _notificator;
        public EditionService(IRepository repository, INotificator notificator)
        {
            _repository = repository;
            _notificator = notificator;
        }
        public bool EditCard(AuthenticationToken token, CardDTO card)
        {
            CardEntity original = _repository.GetItem<CardEntity>(x => x.Id == card.Id);
            if (!Validator.HasAccess(_repository, token, original))
                return false;
            original.Description = card.Description;
            original.CreationDate = card.CreationDate;
            original.Name = card.Name;
            return true;
        }
    }
}
