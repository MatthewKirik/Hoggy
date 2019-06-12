using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Interfaces;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
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
            try
            {
                CardEntity original = _repository.GetItem<CardEntity>(x => x.Id == card.Id);
                if (!Validator.HasAccess(_repository, token, original))
                    return false;
                original.Description = card.Description;
                original.CreationDate = card.CreationDate;
                original.Name = card.Name;
                _repository.Update(original);
                _repository.Save();
                _notificator.WithSecurityGroup(original.SecurityGroupId).OnCardEdited(card);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditColumn(AuthenticationToken token, ColumnDTO column)
        {
            try
            {
                ColumnEntity original = _repository.GetItem<ColumnEntity>(x => x.Id == column.Id);
                if (!Validator.HasAccess(_repository, token, original))
                    return false;
                original.Description = column.Description;
                original.Name = column.Name;
                _repository.Update(original);
                _repository.Save();
                _notificator.WithSecurityGroup(original.SecurityGroupId).OnColumnEdited(column);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditUserProfile(AuthenticationToken token, UserProfileDTO userProfile)
        {
            try
            {
                UserProfileEntity original = _repository.GetItem<UserProfileEntity>(x => x.Id == userProfile.Id);
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
                if (original.User.Id != user.Id)
                    return false;
                original.Phone = userProfile.Phone;
                original.Name = userProfile.Name;
                _repository.Update(original);
                _repository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
