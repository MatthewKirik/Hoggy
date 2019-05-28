using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class TagEntity : BaseSecureEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Colour { get; set; }
        public virtual BoardEntity Board { get; set; }
    }
}
