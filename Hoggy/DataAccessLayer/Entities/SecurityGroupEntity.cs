using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class SecurityGroupEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
