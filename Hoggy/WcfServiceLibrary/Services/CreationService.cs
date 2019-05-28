using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Helpers;

namespace WcfServiceLibrary.Services
{
    public class CreationService : ICreationContract
    {
        private readonly IRepository _repository;
        public CreationService(IRepository repository)
        {
            _repository = repository;
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
            _repository.Add(toAdd);
            _repository.Save();
            return true;
        }

        public bool AddCard(AuthenticationToken token, CardDTO card, int columnId)
        {
            ColumnEntity dest = _repository.GetItem<ColumnEntity>(x => x.Id == columnId);
            if (!Validator.HasAccess<ColumnEntity>(_repository, token, dest))
                return false;
            CardEntity toAdd = AutoMapper.Mapper.Map<CardEntity>(card);
            toAdd.SecurityGroupId = dest.SecurityGroupId;
            _repository.Add(toAdd);
            _repository.Save();
            return true;
        }

        public bool AddColumn(AuthenticationToken token, ColumnDTO column, int boardId)
        {
            BoardEntity dest = _repository.GetItem<BoardEntity>(x => x.Id == boardId);
            if (!Validator.HasAccess<BoardEntity>(_repository, token, dest))
                return false;
            ColumnEntity toAdd = AutoMapper.Mapper.Map<ColumnEntity>(column);
            toAdd.SecurityGroupId = dest.SecurityGroupId;
            _repository.Add(toAdd);
            _repository.Save();
            return true;
        }
    }
}
