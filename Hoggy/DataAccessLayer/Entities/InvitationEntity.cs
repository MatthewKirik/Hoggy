using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_invitations")]
    public class InvitationEntity : BaseEntity
    {
        [Required]
        public virtual UserEntity Recepient { get; set; }   
        [Required]
        public virtual SecurityGroupEntity SecurityGroup { get; set; }
        [Required]
        public virtual UserEntity Sender { get; set; }
    }
}
