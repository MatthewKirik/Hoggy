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
        public virtual ICollection<SecurityGroupEntity> SecurityGroups { get; set; }
        public virtual ICollection<BoardEntity> Boards { get; set; }
        public virtual ICollection<BoardEntity> ParticipatedBoards { get; set; }
        public virtual ICollection<CardEntity> SubscriptedCards { get; set; }
        public virtual ICollection<InvitationEntity> IncomeInvitations { get; set; }
    }
}
