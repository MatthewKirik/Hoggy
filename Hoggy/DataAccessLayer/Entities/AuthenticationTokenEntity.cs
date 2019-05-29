using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_tokens")]
    public class AuthenticationTokenEntity : BaseEntity
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public virtual UserEntity User { get; set; }
    }
}
