using DataAccessLayer.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_profiles")]
    public class UserProfileEntity : BaseEntity
    {
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}
