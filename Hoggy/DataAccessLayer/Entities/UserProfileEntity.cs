using DataAccessLayer.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserProfileEntity : BaseEntity
    {
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}
