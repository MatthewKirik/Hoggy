using System.Collections.Generic;
using System.ServiceModel;
using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DataExchangeService : IDataExchangeContract
    {
        private readonly IRepository _repository;

        public DataExchangeService(IRepository repository)
        {
            _repository = repository;
        }

        public List<BoardDTO> GetBoards(AuthenticationToken token, int UserId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            if (user == null)
                return null;
            if (user.Id == UserId)
                return Mapper.Map<List<BoardDTO>>(user.Boards);
            else
                return null;
        }

        public List<BoardDTO> GetParticipatedBoards(AuthenticationToken token, int UserId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            if (user == null)
                return null;
            if (user.Id == UserId)
                return Mapper.Map<List<BoardDTO>>(user.ParticipatedBoards);
            else
                return null;
        }

        public List<InvitationDTO> GetIncomeInvitations(AuthenticationToken token, int UserId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;

            if (user == null)
                return null;

            if (user.Id == UserId)
            {
                List<InvitationDTO> invitations = new List<InvitationDTO>();
                foreach (var i in user.IncomeInvitations)
                {
                    InvitationDTO invitationDTO = new InvitationDTO()
                    {
                        Id = i.Id,
                        SenderEmail = i.Sender.Email,
                        Key = i.SecurityGroup.Key
                    };

                    invitations.Add(invitationDTO);
                }
                return invitations;
            }
            else
                return null;
        }

        public List<ColumnDTO> GetColumns(AuthenticationToken token, int BoardId)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return null;
            return Mapper.Map<ICollection<ColumnEntity>, List<ColumnDTO>>(board.Columns);
        }

        public List<HistoryEventDTO> GetHistoryEvents(AuthenticationToken token, int BoardId)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return null;
            return Mapper.Map<ICollection<HistoryEventEntity>, List<HistoryEventDTO>>(board.HistoryEvents);
        }

        public List<CardDTO> GetCards(AuthenticationToken token, int ColumnId)
        {
            ColumnEntity column = _repository.GetItem<ColumnEntity>(x => x.Id == ColumnId);
            if (!Validator.HasAccess<ColumnEntity>(_repository, token, column))
                return null;
            return Mapper.Map<ICollection<CardEntity>, List<CardDTO>>(column.Cards);
        }

        public List<UserDTO> GetParticipants(AuthenticationToken token, int BoardId)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return null;
            return Mapper.Map<ICollection<UserEntity>, List<UserDTO>>(board.Participants);
        }

        public List<UserDTO> GetCardSubscribers(AuthenticationToken token, int CardId)
        {
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
            if (!Validator.HasAccess<CardEntity>(_repository, token, card))
                return null;
            return Mapper.Map<ICollection<UserEntity>, List<UserDTO>>(card.Subscribers);
        }

        public List<CommentDTO> GetCardComments(AuthenticationToken token, int CardId)
        {
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
            if (!Validator.HasAccess<CardEntity>(_repository, token, card))
                return null;
            return Mapper.Map<ICollection<CommentEntity>, List<CommentDTO>>(card.Comments);
        }

        public UserDTO GetUser(AuthenticationToken token)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            return AutoMapper.Mapper.Map<UserDTO>(user);
        }

        public List<TagDTO> GetBoardTags(AuthenticationToken token, int BoardId)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return null;
            return Mapper.Map<ICollection<TagEntity>, List<TagDTO>>(board.Tags);
        }

        public List<TagDTO> GetCardTags(AuthenticationToken token, int CardId)
        {
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
            if (!Validator.HasAccess<CardEntity>(_repository, token, card))
                return null;
            return Mapper.Map<ICollection<TagEntity>, List<TagDTO>>(card.Tags);
        }
    }
}
