using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class HistoryEventModel
    {
        public virtual HistoryEventTypeModel EventType { get; set; }
        public virtual UserModel Producer { get; set; }
        public virtual CardModel Card { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ColumnModel Column { get; set; }
    }
}
