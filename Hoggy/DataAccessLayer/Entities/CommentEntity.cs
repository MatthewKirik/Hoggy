using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [Required]
        public virtual UserEntity Author { get; set; }
        [Required]
        public virtual CardEntity Card { get; set; }
    }
}
