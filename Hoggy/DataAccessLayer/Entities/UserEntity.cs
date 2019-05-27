using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        public virtual UserProfileEntity Profile { get; set; }
        public virtual ICollection<SecurityGroupEntity> SecurityGroups { get; set; }
    }
}
