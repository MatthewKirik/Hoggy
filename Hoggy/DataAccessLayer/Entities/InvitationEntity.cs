using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class InvitationEntity : BaseEntity
    {
        [Required]
        public virtual UserEntity Recepient { get; set; }   
        [Required]
        public int SecurityGroupId { get; set; }
    }
}
