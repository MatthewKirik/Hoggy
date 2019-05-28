using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class AuthenticationTokenEntity : BaseEntity
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public virtual UserEntity User { get; set; }
    }
}
