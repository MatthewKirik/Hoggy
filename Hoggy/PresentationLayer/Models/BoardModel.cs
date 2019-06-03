using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class BoardModel : ViewModelBase
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public ObservableCollection<UserModel> Participants { get; set; }
        public ObservableCollection<ColumnModel> Columns { get; set; }

        public BoardModel()
        {
            Columns = new ObservableCollection<ColumnModel>();
        }

    }
}
