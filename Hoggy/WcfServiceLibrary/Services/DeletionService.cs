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

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DeletionService : IDeletionContract
    {
        private readonly IRepository _repository;
        private readonly INotificator _notificator;
        public DeletionService(IRepository repository, INotificator notificator)
        {
            _repository = repository;
            _notificator = notificator;
        }

        public bool DeleteBoard(AuthenticationToken token, int boardId)
        {
            try
            {
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
                BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Creator.Id == boardId);
                if (user == null || board.Creator.Id != user.Id)
                    return false;
                SecurityGroupEntity securityGroup = _repository.GetItem<SecurityGroupEntity>(x => x.Id == board.SecurityGroupId);
                foreach (var col in board.Columns)
                {
                    foreach (var card in col.Cards)
                        _repository.Delete(card);
                    _repository.Delete(col);
                }
                board.Participants.Clear();
                user.Boards.Remove(board);
                _repository.Delete(board);
                _repository.Delete(securityGroup);
                _repository.Save();
                _notificator.WithSecurityGroup(board.SecurityGroupId).OnBoardDeleted(boardId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBoardTag(AuthenticationToken token, int tagId)
        {
            try
            {
                TagEntity tag = _repository.GetItem<TagEntity>(x => x.Id == tagId);
                if (!Validator.HasAccess(_repository, token, tag))
                    return false;
                int boardId = tag.Board.Id;
                tag.Cards.Clear();
                _repository.Delete(tag);
                _repository.Save();
                _notificator.WithSecurityGroup(tag.SecurityGroupId).OnBoardTagDeleted(boardId, tagId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCard(AuthenticationToken token, int cardId)
        {
            try
            {
                CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == cardId);
                if (!Validator.HasAccess(_repository, token, card))
                    return false;
                int boardId = card.Column.Board.Id;
                int columnId = card.Column.Id;
                _repository.Delete(card);
                _repository.Save();
                _notificator.WithSecurityGroup(card.SecurityGroupId).OnCardDeleted(boardId, columnId, cardId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCardTag(AuthenticationToken token, int cardId, int tagId)
        {
            try
            {
                CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == cardId);
                if (!Validator.HasAccess(_repository, token, card))
                    return false;
                TagEntity tag = _repository.GetItem<TagEntity>(x => x.Id == tagId);
                if (!Validator.HasAccess(_repository, token, tag))
                    return false;
                int boardId = card.Column.Board.Id;
                card.Tags.Remove(tag);
                _repository.Update(card);
                _repository.Save();
                _notificator.WithSecurityGroup(card.SecurityGroupId).OnCardTagDeleted(boardId, cardId, tag.Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteColumn(AuthenticationToken token, int columnId)
        {
            try
            {
                ColumnEntity column = _repository.GetItem<ColumnEntity>(x => x.Id == columnId);
                if (!Validator.HasAccess(_repository, token, column))
                    return false;
                foreach (var card in column.Cards.ToList())
                    _repository.Delete(card);
                _repository.Delete(column);
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
