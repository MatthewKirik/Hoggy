using DataAccessLayer.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("tbl_boards")]
    public class BoardEntity : BaseSecureEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        private ICollection<HistoryEventEntity> _historyEvents;
        public virtual ICollection<HistoryEventEntity> HistoryEvents { get => _historyEvents ?? (_historyEvents = new List<HistoryEventEntity>()); }
        private ICollection<UserEntity> _participants;
        public virtual ICollection<UserEntity> Participants { get => _participants ?? (_participants = new List<UserEntity>()); }
        private ICollection<ColumnEntity> _columns;
        public virtual ICollection<ColumnEntity> Columns { get => _columns ?? (_columns = new List<ColumnEntity>()); }
        private ICollection<TagEntity> _tags;
        public virtual ICollection<TagEntity> Tags { get => _tags ?? (_tags = new List<TagEntity>()); }

        public virtual UserEntity Creator { get; set; }
    }
}
