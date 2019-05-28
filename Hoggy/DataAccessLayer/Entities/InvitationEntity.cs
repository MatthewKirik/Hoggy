using DataAccessLayer.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
