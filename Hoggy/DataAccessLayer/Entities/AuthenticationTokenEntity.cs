using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class AuthenticationTokenEntity
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public virtual UserEntity User { get; set; }
    }
}
