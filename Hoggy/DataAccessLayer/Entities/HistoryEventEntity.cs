using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_events")]
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
