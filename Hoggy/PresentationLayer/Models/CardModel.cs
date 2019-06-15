﻿using AutoMapper;
using DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Helpers;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.Models
{
    public class CardModel : ViewModelBase
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
                CanSave = Validator.EmptyStrings(NameErr, DescErr, ExpDateErr);
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
                CanSave = Validator.EmptyStrings(NameErr, DescErr, ExpDateErr);
            }
        }

        public DateTime CreationDate { get; set; }

        private DateTime _expireDate;
        public DateTime ExpireDate
        {
            get => _expireDate;
            set
            {
                _expireDate = value;
                RaisePropertyChanged(nameof(ExpireDate));
                ExpDateErr = Validator.Check<DateTime>(CheckType.DateLessThan, ExpireDate, DateTime.Now);
            }
        }

        private string _expDateErr;
        public string ExpDateErr
        {
            get => _expDateErr;
            set
            {
                _expDateErr = value;
                RaisePropertyChanged(nameof(ExpDateErr));
                CanSave = Validator.EmptyStrings(NameErr, DescErr, ExpDateErr);
            }
        }

        private Color _dateColor;
        public Color DateColor
        {
            get => _dateColor;
            set
            {
                _dateColor = value;
                RaisePropertyChanged(nameof(DateColor));
            }
        }

        public int BoardId { get; set; }
        public int ColumnId { get; set; }

        public ObservableCollection<TagModel> Tags { get; set; }
        public ObservableCollection<TagModel> BoardTags { get; set; }
        public ObservableCollection<CommentModel> Comments { get; set; }
        public ObservableCollection<UserModel> Participants { get; set; }

        public CardModel()
        {
            Tags = new ObservableCollection<TagModel>();
            BoardTags = new ObservableCollection<TagModel>();
            Comments = new ObservableCollection<CommentModel>();
            Participants = new ObservableCollection<UserModel>();
            
            _nameErr = "Empty field";
            _descErr = "Empty field";
            ExpireDate = DateTime.Now;
        }

        //COMMANDS
        private RelayCommand _editCardCmd;
        public RelayCommand EditCardCmd
        {
            get
            {
                return _editCardCmd ?? (_editCardCmd = new RelayCommand(() =>
                {
                    EditCardWindow editCardwindow = new EditCardWindow(this);
                    editCardwindow.ShowDialog();
                }));
            }
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
                ColumnId = ColumnId,
                Tags = new ObservableCollection<TagModel>(Tags),
                BoardTags = BoardTags,
            };
        }
    }
}