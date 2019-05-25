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
        [Required]
        public string Value { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
    }
}
