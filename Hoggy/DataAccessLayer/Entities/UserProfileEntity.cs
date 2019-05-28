using DataAccessLayer.Bases;

namespace DataAccessLayer.Entities
{
    public class UserProfileEntity : BaseEntity
    {
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}
