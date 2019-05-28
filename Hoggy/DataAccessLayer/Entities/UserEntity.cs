using DataAccessLayer.Bases;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }

        public virtual UserProfileEntity Profile { get; set; }

        private ICollection<SecurityGroupEntity> _securityGroups;
        public virtual ICollection<SecurityGroupEntity> SecurityGroups { get => _securityGroups ?? (_securityGroups = new List<SecurityGroupEntity>()); }
        private ICollection<BoardEntity> _boards;
        public virtual ICollection<BoardEntity> Boards { get => _boards ?? (_boards = new List<BoardEntity>()); }
        private ICollection<BoardEntity> _participatedBoards;
        public virtual ICollection<BoardEntity> ParticipatedBoards { get => _participatedBoards ?? (_participatedBoards = new List<BoardEntity>()); }
        private ICollection<CardEntity> _subscriptedCards;
        public virtual ICollection<CardEntity> SubscriptedCards { get => _subscriptedCards ?? (_subscriptedCards = new List<CardEntity>()); }
        private ICollection<InvitationEntity> _incomeInvitations;
        public virtual ICollection<InvitationEntity> IncomeInvitations { get => _incomeInvitations ?? (_incomeInvitations = new List<InvitationEntity>()); }
    }
}
