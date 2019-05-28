using DataAccessLayer.Bases;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class TagEntity : BaseSecureEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Colour { get; set; }

        [Required]
        public virtual BoardEntity Board { get; set; }

        private ICollection<CardEntity> _cards;
        public virtual ICollection<CardEntity> Cards { get => _cards ?? (_cards = new List<CardEntity>()); }
    }
}
