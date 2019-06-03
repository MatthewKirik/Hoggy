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
            UserEntity sender = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            if (recepient == null || sender == null)
                return false;
            SecurityGroupEntity securityGroup = _repository.GetItem<SecurityGroupEntity>(x => x.Id == board.SecurityGroupId);
            InvitationEntity invitation = new InvitationEntity
            {
                Recepient = recepient,
                SecurityGroup = securityGroup,
                Sender = sender
            };
            _repository.Add(invitation);
            recepient.IncomeInvitations.Add(invitation);
            _repository.Update(recepient);
            sender.SentInvitations.Add(invitation);
            _repository.Update(sender);
            securityGroup.Invitations.Add(invitation);
            _repository.Update(securityGroup);
            _repository.Save();
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

        public bool AcceptInvitation(AuthenticationToken token, string key)
        {
            InvitationEntity invitation = _repository.GetItem<InvitationEntity>(x => x.SecurityGroup.Key == key);
            UserEntity recepient = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            if (invitation == null || recepient == null)
                return false;

            recepient.IncomeInvitations.Remove(invitation);
            invitation.Sender.SentInvitations.Remove(invitation);
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.SecurityGroupId == invitation.SecurityGroup.Id);
            board.Participants.Add(recepient);
            recepient.ParticipatedBoards.Add(board);
            invitation.SecurityGroup.Invitations.Remove(invitation);
            invitation.SecurityGroup.Users.Add(recepient);
            recepient.SecurityGroups.Add(invitation.SecurityGroup);
            _repository.Update(invitation.SecurityGroup);
            _repository.Update(invitation.Sender);
            _repository.Update(recepient);
            _repository.Update(board);
            _repository.Delete(invitation);
            _repository.Save();
            return true;
        }
    }
}
