using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class BoardEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public virtual SecurityGroupEntity SecurityGroup { get; set; }
        public virtual ICollection<UserEntity> Participants { get; set; }
        public virtual ICollection<ColumnEntity> Columns { get; set; }
    }
}
