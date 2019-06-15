using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PresentationLayer.Models
{
    public class BoardModel : ViewModelBase, ICloneable
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
                NameErr = Validator.Check<int>(CheckType.Empty, Name);
            }
        }

        private string _nameErr;
        public string NameErr
        {
            get => _nameErr;
            set
            {
                _nameErr = value;
                RaisePropertyChanged(nameof(NameErr));
                CanSave = Validator.EmptyStrings(NameErr, DescErr);
            }
        }

        bool _canSave;
        public bool CanSave
        {
            get => _canSave;
            set
            {
                _canSave = value;
                RaisePropertyChanged(nameof(CanSave));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
                DescErr = Validator.Check<int>(CheckType.Empty, Description);
            }
        }

        private string _descErr;
        public string DescErr
        {
            get => _descErr;
            set
            {
                _descErr = value;
                RaisePropertyChanged(nameof(DescErr));
                CanSave = Validator.EmptyStrings(NameErr, DescErr);
            }
        }
        
        public DateTime CreationDate { get; set; }
        public ObservableCollection<UserModel> Participants { get; set; }
        public ObservableCollection<ColumnModel> Columns { get; set; }
        public ObservableCollection<TagModel> Tags { get; set; }
        public ObservableCollection<HistoryEventModel> HistoryEvents { get; set; }

        public BoardModel()
        {
            Tags = new ObservableCollection<TagModel>();
            Columns = new ObservableCollection<ColumnModel>();
            Participants = new ObservableCollection<UserModel>();
            HistoryEvents = new ObservableCollection<HistoryEventModel>();
            _nameErr = "Empty field"; 
            _descErr = "Empty field";
        }

        public Action<int> ChangeBoard;
  
        //COMMANDS
        private RelayCommand _changeBoardCmd;
        public RelayCommand ChangeBoardCmd
        {
            get
            {
                return _changeBoardCmd ?? (_changeBoardCmd = new RelayCommand(() =>
                {
                    ChangeBoard(Id);
                }));
            }
        }

        public object Clone()
        {
            return new BoardModel
            {
                Id = Id,
                Name = Name,
                Description = Description,
                CreationDate = CreationDate,
                Participants = Participants
            };
        }
    }
}
