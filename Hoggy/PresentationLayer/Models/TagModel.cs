using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.Models
{
    public class TagModel : ViewModelBase
    {
        private string _name;
        
        public int Id { get; set; }

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
                CanSave = Validator.EmptyStrings(NameErr);
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

        Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                RaisePropertyChanged(nameof(Color));
            }
        }

        public Action<TagModel> AddTagToCardAct;

        public TagModel()
        {
            _nameErr = "Name is empty";
        }


        //COMMANDS
        private RelayCommand _addTagToCartCmd;
        public RelayCommand AddTagToCartCmd
        {
            get
            {
                return _addTagToCartCmd ?? (_addTagToCartCmd = new RelayCommand(() =>
                {
                    AddTagToCardAct.Invoke(this);
                }));
            }
        }
    }
}