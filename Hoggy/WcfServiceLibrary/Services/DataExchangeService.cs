using System.Collections.Generic;
using System.ServiceModel;
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
            {
                List<BoardDTO> boards = new List<BoardDTO>();
                foreach (var b in user.Boards)
                {
                    boards.Add(AutoMapper.Mapper.Map<BoardDTO>(b));
                }
                return boards;
            }
            else
                return null;
        }

        public List<BoardDTO> GetParticipatedBoards(AuthenticationToken token, int UserId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;

            if (user == null)
                return null;

            if (user.Id == UserId)
            {
                List<BoardDTO> boards = new List<BoardDTO>();
                foreach (var b in user.ParticipatedBoards)
                {
                    boards.Add(AutoMapper.Mapper.Map<BoardDTO>(b));
                }
                return boards;
            }
            else
                return null;
        }

        public List<ColumnDTO> GetColumns(AuthenticationToken token, int BoardId)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return null;

            List<ColumnDTO> columns = new List<ColumnDTO>();
            foreach (var c in board.Columns)
            {
                columns.Add(AutoMapper.Mapper.Map<ColumnDTO>(c));
            }
            return columns;
        }

        public List<HistoryEventDTO> GetHistoryEvents(AuthenticationToken token, int BoardId)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return null;


            List<HistoryEventDTO> historyEvents = new List<HistoryEventDTO>();
            foreach (var e in board.HistoryEvents)
            {
                historyEvents.Add(AutoMapper.Mapper.Map<HistoryEventDTO>(e));
            }
            return historyEvents;
        }

        public List<CardDTO> GetCards(AuthenticationToken token, int ColumnId)
        {
            ColumnEntity column = _repository.GetItem<ColumnEntity>(x => x.Id == ColumnId);
            if (!Validator.HasAccess<ColumnEntity>(_repository, token, column))
                return null;

            List<CardDTO> cards = new List<CardDTO>();
            foreach (var c in column.Cards)
            {
                cards.Add(AutoMapper.Mapper.Map<CardDTO>(c));
            }
            return cards;
        }

        public List<UserDTO> GetParticipants(AuthenticationToken token, int BoardId)
        {
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, board))
                return null;

            List<UserDTO> paricipants = new List<UserDTO>();
            foreach (var u in board.Participants)
            {
                paricipants.Add(AutoMapper.Mapper.Map<UserDTO>(u));
            }
            return paricipants;
        }

        public List<UserDTO> GetCardSubscribers(AuthenticationToken token, int CardId)
        {
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
            if (!Validator.HasAccess<CardEntity>(_repository, token, card))
                return null;
            
            List<UserDTO> subscribers = new List<UserDTO>();
            foreach (var u in card.Subscribers)
            {
                subscribers.Add(AutoMapper.Mapper.Map<UserDTO>(u));
            }
            return subscribers;
        }

        public List<CommentDTO> GetCardComments(AuthenticationToken token, int CardId)
        {
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
            if (!Validator.HasAccess<CardEntity>(_repository, token, card))
                return null;

            List<CommentDTO> comments = new List<CommentDTO>();
            foreach (var c in card.Comments)
            {
                comments.Add(AutoMapper.Mapper.Map<CommentDTO>(c));
            }
            return comments;
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

            List<TagDTO> tags = new List<TagDTO>();
            foreach (var c in board.Tags)
            {
                tags.Add(AutoMapper.Mapper.Map<TagDTO>(c));
            }
            return tags;
        }

        public List<TagDTO> GetCardTags(AuthenticationToken token, int CardId)
        {
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
            if (!Validator.HasAccess<CardEntity>(_repository, token, card))
                return null;

            List<TagDTO> tags = new List<TagDTO>();
            foreach (var c in card.Tags)
            {
                tags.Add(AutoMapper.Mapper.Map<TagDTO>(c));
            }
            return tags;
        }
    }
}
