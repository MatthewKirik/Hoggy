using System.ServiceModel;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CommunityService : ICommunityContract
    {
        private readonly IRepository _repository;
        public CommunityService(IRepository repository)
        {
            _repository = repository;
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

        public bool SubscribeToCard(AuthenticationToken token, CommentDTO comment, int cardId)
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
            return true;
        }
    }
}
