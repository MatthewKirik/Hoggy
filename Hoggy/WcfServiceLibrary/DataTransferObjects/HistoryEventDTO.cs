using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.DataTransferObjects
{
    public class HistoryEventDTO
    {
        public virtual HistoryEventTypeDTO EventType { get; set; }
        public virtual UserDTO Producer { get; set; }
        public virtual CardDTO Card { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ColumnDTO Column { get; set; }
    }
}
