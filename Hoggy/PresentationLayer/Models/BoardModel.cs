using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class BoardModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public UserModel Creator { get; set; }
        public ObservableCollection<UserModel> Participants { get; set; }
        public ObservableCollection<ColumnModel> Columns { get; set; }

    }
}
