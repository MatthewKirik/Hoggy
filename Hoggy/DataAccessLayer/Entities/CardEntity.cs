using DataAccessLayer.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class CardEntity : BaseSecureEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        public virtual TagEntity Tag { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
        public virtual ICollection<UserEntity> Subscribers { get; set; }
        public virtual ColumnEntity Column { get; set; }
    }
}
