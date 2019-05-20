using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    class HistoryEventTypeEntity
    {
        public int Id { get; set; }
        [Required]
        public virtual HistoryEventTypeEntity EventType { get; set; }
        [Required]
        public virtual UserEntity Producer { get; set; }
        public virtual CardEntity Card { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual ColumnEntity Column { get; set; }
    }
}
