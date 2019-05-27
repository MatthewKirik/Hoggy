using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual BoardEntity Board { get; set; }
        [Required]
        public virtual SecurityGroupEntity SecurityGroup { get; set; }
    }
}
