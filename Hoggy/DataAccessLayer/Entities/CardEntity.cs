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

        public virtual ColumnEntity Column { get; set; }
        public virtual TagEntity Tag { get; set; }

        private ICollection<CommentEntity> _comments;
        public virtual ICollection<CommentEntity> Comments { get => _comments ?? (_comments = new List<CommentEntity>()); }
        private ICollection<UserEntity> _subscribers;
        public virtual ICollection<UserEntity> Subscribers { get => _subscribers ?? (_subscribers = new List<UserEntity>()); }
        private ICollection<TagEntity> _tags;
        public virtual ICollection<TagEntity> Tags { get => _tags ?? (_tags = new List<TagEntity>()); }
    }
}