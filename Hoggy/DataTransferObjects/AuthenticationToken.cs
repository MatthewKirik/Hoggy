using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class AuthenticationToken
    {
        public string Value { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
