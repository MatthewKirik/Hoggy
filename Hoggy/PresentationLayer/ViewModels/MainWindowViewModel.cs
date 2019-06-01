using DataTransferObjects;
using GalaSoft.MvvmLight;
using PresentationLayer.AuthenticationService;
using PresentationLayer.DataExchangeService;
using PresentationLayer.Models;
using PresentationLayer.RegistrationService;
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

        bool _loaderVisible;
        public bool LoaderVisible
        {
            get => _loaderVisible;
            set
            {
                _loaderVisible = value;
                RaisePropertyChanged(nameof(LoaderVisible));
            }
        }

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

        AuthenticationToken token;
        Window _mainWindow;

        public RegistrationContractClient RegProxy { get; }
        public AuthenticationContractClient AuthProxy { get; }
        public DataExchangeContractClient DataExchangeProxy { get; }

        public MainWindowViewModel(Window mainWindow)
        {
            DataExchangeProxy = new DataExchangeContractClient();
            _mainWindow = mainWindow;

            RegistrationWindow auth = new RegistrationWindow();
            if (auth.ShowDialog() != true)
                _mainWindow.Close();
            RegistrationWindowViewModel regModel = (RegistrationWindowViewModel)auth.DataContext;

            token = regModel.Token;
            User = new UserModel();
            GetUser();


            
            AvaPath = @"..\Resources\default_ava.jpg";
           
            //CurBoard = (User.Boards.Count >0) ? User.Boards[0] : null;

            ObservableCollection<BoardModel> _bds = new ObservableCollection<BoardModel>
            {
                new BoardModel{Name="FirstBoard"},
                new BoardModel{Name="SecondBoard"}
            };

        }

        void GetUser()
        {
            LoaderVisible = true;
            Task.Run(() => {
                try
                {
                    UserDTO userDTO = DataExchangeProxy.GetUser(token);
                    if (userDTO != null)
                    {
                        User.Id = userDTO.Id;
                        User.Login = userDTO.Login;
                        User.Email = userDTO.Email;
                    }
                    else
                        MessageBox.Show("Cant load user");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoaderVisible = false;
            });
        }

    }
}
