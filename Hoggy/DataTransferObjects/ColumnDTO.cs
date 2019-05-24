using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class ColumnDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CardDTO> Cards { get; set; }
    }
}
