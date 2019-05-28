using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Bases
{
    public abstract class BaseSecureEntity : BaseEntity
    {
        [Required]
        public int SecurityGroupId { get; set; }
    }
}
