using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class HistoryEventEntity : BaseSecureEntity
    {
        [Required]
        public virtual UserEntity Producer { get; set; }
        [Required]
        public virtual BoardEntity Board { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
