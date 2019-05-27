using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class HistoryEventEntity
    {
        public int Id { get; set; }
        [Required]
        public virtual UserEntity Producer { get; set; }
        [Required]
        public virtual BoardEntity Board { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
