using DataAccessLayer.Bases;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class ColumnEntity : BaseSecureEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CardEntity> Cards { get; set; }
        [Required]
        public virtual BoardEntity Board { get; set; }
    }
}
