using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ColumnModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<CardModel> Cards { get; set; }
    }
}
