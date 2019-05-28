using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
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
