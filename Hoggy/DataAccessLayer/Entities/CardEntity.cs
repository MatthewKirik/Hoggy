using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public virtual ICollection<TagEntity> Tags { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
        public virtual ICollection<UserEntity> Participants { get; set; }
        public virtual ColumnEntity Column { get; set; }
    }
}
