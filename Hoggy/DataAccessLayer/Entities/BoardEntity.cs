using DataAccessLayer.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class BoardEntity : BaseSecureEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public virtual ICollection<HistoryEventEntity> HistoryEvents { get; set; }
        public virtual ICollection<UserEntity> Participants { get; set; }
        public virtual UserEntity Creator { get; set; }
        public virtual ICollection<ColumnEntity> Columns { get; set; }
    }
}
