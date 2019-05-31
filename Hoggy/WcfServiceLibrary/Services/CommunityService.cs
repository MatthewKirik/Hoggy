using System.ServiceModel;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Interfaces;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CommunityService : ICommunityContract
    {
        private readonly IRepository _repository;
        private readonly INotificator _notificator;
        public CommunityService(IRepository repository, INotificator notificator)
        {
            _repository = repository;
            _notificator = notificator;
        }

        public bool PostComment(AuthenticationToken token, CommentDTO comment, int cardId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            CardEntity dest = _repository.GetItem<CardEntity>(x => x.Id == cardId);
            if (!Validator.HasAccess<ColumnEntity>(_repository, token, dest))
                return false;
            CommentEntity toAdd = AutoMapper.Mapper.Map<CommentEntity>(comment);
            toAdd.SecurityGroupId = dest.SecurityGroupId;
            toAdd.Author = user;
            _repository.Add(toAdd);
            _repository.Save();
            dest.Comments.Add(toAdd);
            _repository.Update(dest);
            _repository.Save();
            _notificator.WithSecurityGroup(dest.SecurityGroupId).OnCardCommentAdded(comment, cardId);
            return true;
        }

        public bool SendInvitation(AuthenticationToken token, int boardId, string email)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == boardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return false;
            UserEntity recepient = _repository.GetItem<UserEntity>(x => x.Email == email);
            if (recepient == null)
                return false;

            InvitationEntity invitation = new InvitationEntity();
            invitation.Recepient = recepient;
            invitation.SecurityGroupId = board.SecurityGroupId;
            
            _repository.Add(invitation);
            return true;
        }

        public bool SubscribeToCard(AuthenticationToken token, int cardId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == cardId);
            if (!Validator.HasAccess<CardEntity>(_repository, token, card))
                return false;
            card.Subscribers.Add(user);
            user.SubscriptedCards.Add(card);
            _repository.Update(card);
            _repository.Update(user);
            _repository.Save();
            _notificator.WithSecurityGroup(card.SecurityGroupId).OnCardSubscribersAdded(AutoMapper.Mapper.Map<UserDTO>(user), cardId);
            return true;
        }

        public bool AddTagToCard(AuthenticationToken token, int tagId, int cardId)
        {
            CardEntity dest = _repository.GetItem<CardEntity>(x => x.Id == cardId);
            TagEntity tag = _repository.GetItem<TagEntity>(x => x.Id == tagId);
            if (!Validator.HasAccess<ColumnEntity>(_repository, token, dest))
                return false;
            if (!Validator.HasAccess<TagEntity>(_repository, token, tag))
                return false;

            tag.Cards.Add(dest);
            dest.Tags.Add(tag);
            _repository.Update(tag);
            _repository.Update(dest);
            _repository.Save();
            _notificator.WithSecurityGroup(dest.SecurityGroupId).OnCardTagAdded(tagId, cardId);
            return true;
        }
    }
}
