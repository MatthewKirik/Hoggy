using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Interfaces;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CreationService : ICreationContract
    {
        private readonly IRepository _repository;
        private readonly INotificator _notificator;
        public CreationService(IRepository repository, INotificator notificator)
        {
            _repository = repository;
            _notificator = notificator;
        }

        public bool AddBoard(AuthenticationToken token, BoardDTO board)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            if (user == null)
                return false;

            SecurityGroupEntity securityGroup = new SecurityGroupEntity();
            securityGroup.Users.Add(user);
            SHA256Managed cryptor = new SHA256Managed();
            string hashStr = user.Login + DateTime.Now.ToString() + "superSaltHoggy" + user.Password;
            securityGroup.Key = Encoding.Unicode.GetString(cryptor.ComputeHash(Encoding.Unicode.GetBytes(hashStr)));
            _repository.Add(securityGroup);
            _repository.Save();

            BoardEntity toAdd = AutoMapper.Mapper.Map<BoardEntity>(board);
            toAdd.SecurityGroupId = securityGroup.Id;
            toAdd.Creator = user;
            _repository.Add(toAdd);
            _repository.Update(user);
            _repository.Save();
            _notificator.OnBoardAdded(board);
            return true;
        }

        public bool AddCard(AuthenticationToken token, CardDTO card, int columnId)
        {
            ColumnEntity dest = _repository.GetItem<ColumnEntity>(x => x.Id == columnId);
            if (!Validator.HasAccess(_repository, token, dest))
                return false;
            CardEntity toAdd = AutoMapper.Mapper.Map<CardEntity>(card);
            toAdd.SecurityGroupId = dest.SecurityGroupId;
            toAdd.Column = dest;
            _repository.Add(toAdd);
            _repository.Save();
            _repository.Update(dest);
            _repository.Save();
            _notificator.WithSecurityGroup(dest.SecurityGroupId).OnCardAdded(card, columnId);
            HistoryEventEntity historyEvent = new HistoryEventEntity()
            {
                Board = dest.Board,
                Producer = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User,
                SecurityGroupId = dest.SecurityGroupId,
                Text = $"Card \"{toAdd.Name}\" added"
            };
            _repository.Add(historyEvent);
            _repository.Save();
            _notificator.WithSecurityGroup(dest.SecurityGroupId)
                .OnHistoryEventAdded(Mapper.Map<HistoryEventEntity, HistoryEventDTO>(historyEvent), dest.Board.Id);
            return true;
        }

        public bool AddColumn(AuthenticationToken token, ColumnDTO column, int boardId)
        {
            BoardEntity dest = _repository.GetItem<BoardEntity>(x => x.Id == boardId);
            if (!Validator.HasAccess(_repository, token, dest))
                return false;
            ColumnEntity toAdd = Mapper.Map<ColumnEntity>(column);
            toAdd.SecurityGroupId = dest.SecurityGroupId;
            toAdd.Board = dest;
            _repository.Add(toAdd);
            _repository.Save();
            _repository.Update(dest);
            _repository.Save();
            column.Id = toAdd.Id;
            _notificator.WithSecurityGroup(dest.SecurityGroupId).OnColumnAdded(column, boardId);
            HistoryEventEntity historyEvent = new HistoryEventEntity()
            {
                Board = dest,
                Producer = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User,
                SecurityGroupId = dest.SecurityGroupId,
                Text = $"Column \"{toAdd.Name}\" added"
            };
            _repository.Add(historyEvent);
            _repository.Save();
            _notificator.WithSecurityGroup(dest.SecurityGroupId)
                .OnHistoryEventAdded(Mapper.Map<HistoryEventEntity, HistoryEventDTO>(historyEvent), boardId);
            return true;
        }

        public bool AddTagToBoard(AuthenticationToken token, TagDTO tag, int boardId)
        {
            BoardEntity dest = _repository.GetItem<BoardEntity>(x => x.Id == boardId);
            if (!Validator.HasAccess(_repository, token, dest))
                return false;
            TagEntity toAdd = AutoMapper.Mapper.Map<TagEntity>(tag);
            toAdd.SecurityGroupId = dest.SecurityGroupId;
            toAdd.Board = dest;
            _repository.Add(toAdd);
            _repository.Update(dest);
            _repository.Save();
            _notificator.WithSecurityGroup(dest.SecurityGroupId).OnBoardTagAdded(tag, boardId);
            return true;
        }
    }
}
