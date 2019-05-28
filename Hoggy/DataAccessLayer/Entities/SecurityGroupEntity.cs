using DataAccessLayer.Bases;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class SecurityGroupEntity : BaseEntity
    {
        [Required]
        public string Key { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
