using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.Models
{
    public class CardModel : ViewModelBase, ICloneable
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
        public DateTime ExpireDate { get; set; }
        public Color DateColor { get; set; }
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
                    EditCard editCardwindow = new EditCard((CardModel)Clone());
                    editCardwindow.ShowDialog();


                }));
            }
        }

        public CardModel()
        {
            Tags = new ObservableCollection<TagModel>();
            Comments = new ObservableCollection<CommentModel>();
            Participants = new ObservableCollection<UserModel>();
        }

        public object Clone()
        {
            return new CardModel
            {
                Id = Id,
                Name = Name,
                Description = Description,
                CreationDate = CreationDate,
                ExpireDate = ExpireDate,
                DateColor = DateColor,
                Tags = new ObservableCollection<TagModel>(Tags),
                Comments = new ObservableCollection<CommentModel>(Comments),
                Participants = new ObservableCollection<UserModel>(Participants)
            };
        }
    }
}
