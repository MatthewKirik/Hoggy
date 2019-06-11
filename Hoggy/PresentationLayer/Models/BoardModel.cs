﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        public ObservableCollection<TagModel> Tags { get; set; }

        public BoardModel()
        {
            Tags = new ObservableCollection<TagModel>();
            Columns = new ObservableCollection<ColumnModel>();
            Participants = new ObservableCollection<UserModel>();
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

    }
}
