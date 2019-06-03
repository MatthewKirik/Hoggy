using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public ObservableCollection<TagModel> Tags { get; set; }
        public ObservableCollection<CommentModel> Comments { get; set; }
        public ObservableCollection<UserModel> Participants { get; set; }

        
        //COMMANDS
        private RelayCommand _editCardCmd;
        public RelayCommand EditCardCmd
        {
            get
            {
                return _editCardCmd ?? (_editCardCmd = new RelayCommand(() =>
                {
                    MessageBox.Show("Edit!");
                }));
            }
        }
    }
}
