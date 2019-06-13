﻿using System.Collections.Generic;
using System.ServiceModel;
using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataTransferObjects;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;

namespace WcfServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DataExchangeService : IDataExchangeContract
    {
        private readonly IRepository _repository;

        public DataExchangeService(IRepository repository)
        {
            _repository = repository;
        }

        public List<BoardDTO> GetBoards(AuthenticationToken token, int UserId)
        {
            try
            {
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
                if (user == null)
                    return null;
                if (user.Id == UserId)
                    return Mapper.Map<List<BoardDTO>>(user.Boards);
                else
                    return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<BoardDTO> GetParticipatedBoards(AuthenticationToken token, int UserId)
        {
            try
            {
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
                if (user == null)
                    return null;
                if (user.Id == UserId)
                    return Mapper.Map<List<BoardDTO>>(user.ParticipatedBoards);
                else
                    return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<InvitationDTO> GetIncomeInvitations(AuthenticationToken token, int UserId)
        {
            try
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
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<ColumnDTO> GetColumns(AuthenticationToken token, int BoardId)
        {
            try
            {
                BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
                if (!Validator.HasAccess(_repository, token, board))
                    return null;
                return Mapper.Map<ICollection<ColumnEntity>, List<ColumnDTO>>(board.Columns);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<HistoryEventDTO> GetHistoryEvents(AuthenticationToken token, int BoardId)
        {
            try
            {
                BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
                if (!Validator.HasAccess(_repository, token, board))
                    return null;
                return Mapper.Map<ICollection<HistoryEventEntity>, List<HistoryEventDTO>>(board.HistoryEvents);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<CardDTO> GetCards(AuthenticationToken token, int ColumnId)
        {
            try
            {
                ColumnEntity column = _repository.GetItem<ColumnEntity>(x => x.Id == ColumnId);
                if (!Validator.HasAccess(_repository, token, column))
                    return null;
                return Mapper.Map<ICollection<CardEntity>, List<CardDTO>>(column.Cards);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<UserDTO> GetParticipants(AuthenticationToken token, int BoardId)
        {
            try
            {
                BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
                if (!Validator.HasAccess(_repository, token, board))
                    return null;
                return Mapper.Map<ICollection<UserEntity>, List<UserDTO>>(board.Participants);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<UserDTO> GetCardSubscribers(AuthenticationToken token, int CardId)
        {
            try
            {
                CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
                if (!Validator.HasAccess(_repository, token, card))
                    return null;
                return Mapper.Map<ICollection<UserEntity>, List<UserDTO>>(card.Subscribers);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<CommentDTO> GetCardComments(AuthenticationToken token, int CardId)
        {
            try
            {
                CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
                if (!Validator.HasAccess(_repository, token, card))
                    return null;
                return Mapper.Map<ICollection<CommentEntity>, List<CommentDTO>>(card.Comments);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public UserDTO GetUser(AuthenticationToken token)
        {
            try
            {
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
                return Mapper.Map<UserDTO>(user);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public UserProfileDTO GetUserProfile(AuthenticationToken token)
        {
            try
            {
                UserEntity user = _repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;
                UserProfileEntity profile = user.Profile;
                UserProfileDTO userProfileDTO = new UserProfileDTO()
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    Phone = profile.Phone
                };
                return userProfileDTO;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<TagDTO> GetBoardTags(AuthenticationToken token, int BoardId)
        {
            try
            {
                BoardEntity board = _repository.GetItem<BoardEntity>(x => x.Id == BoardId);
                if (!Validator.HasAccess(_repository, token, board))
                    return null;
                return Mapper.Map<ICollection<TagEntity>, List<TagDTO>>(board.Tags);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<TagDTO> GetCardTags(AuthenticationToken token, int CardId)
        {
            try
            {
                CardEntity card = _repository.GetItem<CardEntity>(x => x.Id == CardId);
                if (!Validator.HasAccess(_repository, token, card))
                    return null;
                return Mapper.Map<ICollection<TagEntity>, List<TagDTO>>(card.Tags);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
