using DataAccessLayer.Bases;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_columns")]
    public class ColumnEntity : BaseSecureEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        private ICollection<CardEntity> _cards;
        public virtual ICollection<CardEntity> Cards { get => _cards ?? (_cards = new List<CardEntity>()); }

        [Required]
        public virtual BoardEntity Board { get; set; }
    }
}
