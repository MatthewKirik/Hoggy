using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ColumnEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int SecurityGroupId { get; set; }
        public virtual ICollection<CardEntity> Cards { get; set; }
        [Required]
        public virtual BoardEntity Board { get; set; }
    }
}
