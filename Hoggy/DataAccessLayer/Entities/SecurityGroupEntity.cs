using DataAccessLayer.Bases;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_securityGroups")]
    public class SecurityGroupEntity : BaseEntity
    {
        [Required]
        public string Key { get; set; }

        private ICollection<UserEntity> _users;
        public virtual ICollection<UserEntity> Users { get => _users ?? (_users = new List<UserEntity>()); }
        private ICollection<InvitationEntity> _invitations;
        public virtual ICollection<InvitationEntity> Invitations { get => _invitations ?? (_invitations = new List<InvitationEntity>()); }
    }
}
