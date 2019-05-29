using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_comments")]
    public class CommentEntity : BaseSecureEntity
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public virtual UserEntity Author { get; set; }
        [Required]
        public virtual CardEntity Card { get; set; }
    }
}
