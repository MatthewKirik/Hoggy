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

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DataExchangeService : IDataExchangeContract
    {
        IRepository _repository;

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
                foreach (var b in user.Profile.Boards)
                {
                    BoardDTO boardDTO = new BoardDTO
                    {
                        CreationDate = b.CreationDate,
                        Description = b.Description,
                        Name = b.Name,
                        Id = b.Id
                    };
                    boards.Add(boardDTO);
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
                foreach (var b in user.Profile.ParticipatedBoards)
                {
                    BoardDTO boardDTO = new BoardDTO
                    {
                        CreationDate = b.CreationDate,
                        Description = b.Description,
                        Name = b.Name,
                        Id = b.Id
                    };
                    boards.Add(boardDTO);
                }
                return boards;
            }
            else
                return null;
        }

        public List<ColumnDTO> GetColumns(AuthenticationToken token, int BoardId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);

            if (user == null || board == null)
                return null;

            if (board.SecurityGroup.Users.Contains(user))
            {
                List<ColumnDTO> columns = new List<ColumnDTO>();
                foreach (var c in board.Columns)
                {
                    ColumnDTO columnDTO = new ColumnDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description
                    };
                    columns.Add(columnDTO);
                }
                return columns;
            }
            else
                return null;
        }

        public List<HistoryEventDTO> GetHistoryEvents(AuthenticationToken token, int BoardId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);

            if (user == null || board == null)
                return null;

            if (board.SecurityGroup.Users.Contains(user))
            {
                List<HistoryEventDTO> historyEvents = new List<HistoryEventDTO>();
                foreach (var e in board.HistoryEvents)
                {
                    HistoryEventDTO historyEventDTO = new HistoryEventDTO
                    {
                        ProducerLogin = e.Producer.Login.Clone() as string,
                        Text = e.Text.Clone() as string
                    };
                    historyEvents.Add(historyEventDTO);
                }
                return historyEvents;
            }
            else
                return null;
        }

        public List<CardDTO> GetCards(AuthenticationToken token, int ColumnId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            ColumnEntity column = _repository.GetItem<ColumnEntity>(x => x.Id == ColumnId);

            if (user == null || column == null)
                return null;

            if (column.SecurityGroup.Users.Contains(user))
            {
                List<CardDTO> cards = new List<CardDTO>();
                foreach (var c in column.Cards)
                {
                    TagDTO tagDTO = new TagDTO();
                    tagDTO.Name = c.Tag.Name;
                    tagDTO.Id = c.Tag.Id;
                    CardDTO cardDTO = new CardDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreationDate = c.CreationDate,
                        Description = c.Description,
                        ExpireDate = c.ExpireDate,
                        Tag = tagDTO
                    };
                    cards.Add(cardDTO);
                }
                return cards;
            }
            else
                return null;
        }

        public List<UserDTO> GetParticipants(AuthenticationToken token, int BoardId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);

            if (user == null || board == null)
                return null;

            if (board.SecurityGroup.Users.Contains(user))
            {
                List<UserDTO> paricipants = new List<UserDTO>();
                foreach (var u in board.Participants)
                {
                    UserDTO userDTO = new UserDTO
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Login = u.Login
                    };
                    paricipants.Add(userDTO);
                }
                return paricipants;
            }
            else
                return null;
        }

        public List<UserDTO> GetCardSubscribers(AuthenticationToken token, int CardId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);

            if (user == null || card == null)
                return null;

            if (card.SecurityGroup.Users.Contains(user))
            {
                List<UserDTO> subscribers = new List<UserDTO>();
                foreach (var u in card.Subscribers)
                {
                    UserDTO userDTO = new UserDTO
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Login = u.Login
                    };
                    subscribers.Add(userDTO);
                }
                return subscribers;
            }
            else
                return null;
        }

        public List<CommentDTO> GetCardComments(AuthenticationToken token, int CardId)
        {
            UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
            CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);

            if (user == null || card == null)
                return null;

            if (card.SecurityGroup.Users.Contains(user))
            {
                List<CommentDTO> comments = new List<CommentDTO>();
                foreach (var c in card.Comments)
                {
                    CommentDTO userDTO = new CommentDTO
                    {
                        Id = c.Id,
                        Text = c.Text
                    };
                    comments.Add(userDTO);
                }
                return comments;
            }
            else
                return null;
        }
    }
}
