using DataTransferObjects;
using GalaSoft.MvvmLight;
using PresentationLayer.Models;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<BoardModel> Boards { get; set; }

        UserModel _user;
        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(nameof(User));
            }
        }

        BoardModel _curBoard;
        public BoardModel CurBoard
        {
            get => _curBoard;
            set
            {
                _curBoard = value;
                RaisePropertyChanged(nameof(CurBoard));
            }
        }

        string _avaPath;
        public string AvaPath
        {
            get => _avaPath;
            set
            {
                _avaPath = value;
                RaisePropertyChanged(nameof(AvaPath));
            }
        }

        Window _mainWindow;
        public MainWindowViewModel(Window mainWindow)
        {
            _mainWindow = mainWindow;
            RegistrationWindow auth = new RegistrationWindow();
            if (auth.ShowDialog() != true)
                _mainWindow.Close();

            AvaPath = @"..\Resources\default_ava.jpg";
            ///LOCAL TESTING VALUES
            ObservableCollection<BoardModel> _bds = new ObservableCollection<BoardModel>
            {
                new BoardModel{Name="FirstBoard" },
                new BoardModel{Name="SecondBoard"}
            };

            _bds[0].Columns = new ObservableCollection<ColumnModel>
            {
                new ColumnModel{Name="Column1", Cards = new ObservableCollection<CardModel>{
                    new CardModel{Name = "Card1", Description = "Desc 1", Tags = new ObservableCollection<TagModel>{
                        new TagModel{Name = "Tag1", Color = (Color)ColorConverter.ConvertFromString("#fbb")}
                    }, },
                    new CardModel{Name = "Card2", Description = "Desc 2", Tags = new ObservableCollection<TagModel>{
                        new TagModel{Name = "Tag1", Color = (Color)ColorConverter.ConvertFromString("#f00")}
                    }, }
                }, },
                new ColumnModel{Name="Column2", Cards = new ObservableCollection<CardModel>{
                    new CardModel{Name = "Card3", Description = "Desc 3", Tags = new ObservableCollection<TagModel>{
                        new TagModel{Name = "Tag3", Color = (Color)ColorConverter.ConvertFromString("#00f")}
                    }, },
                    new CardModel{Name = "Card2", Description = "Desc 2", Tags = new ObservableCollection<TagModel>{
                        new TagModel{Name = "Tag1", Color = (Color)ColorConverter.ConvertFromString("#f0d")}
                    }, }
                }, }
            };

            User = new UserModel { Login = "Pasha", Boards = _bds,Email="test@test.ua", Password="XYZ" };

            CurBoard = (User.Boards.Count >0) ? User.Boards[0] : null;
        }

    }
}
