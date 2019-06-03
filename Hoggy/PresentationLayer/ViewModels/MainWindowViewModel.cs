using AutoMapper;
using DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.AuthenticationService;
using PresentationLayer.DataExchangeService;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using PresentationLayer.RegistrationService;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
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
            MapperConfigurator.Configure();

            RegistrationWindow auth = new RegistrationWindow();
            if (auth.ShowDialog() != true)
                _mainWindow.Close();
            RegistrationWindowViewModel regModel = (RegistrationWindowViewModel)auth.DataContext;

            token = regModel.Token;
            User = new UserModel();

            GetUser();
            GetBoards(nameof(User.Boards));
            GetBoards(nameof(User.PartBoards));
            
            if (User.Boards.Count + User.PartBoards.Count > 0)
                CurBoard = (User.Boards.Count > 0) ? User.Boards[0] : User.PartBoards[0];
            
            if (CurBoard != null)
            {
                GetColumns(CurBoard.Id);
                if (CurBoard.Columns.Count > 0)
                    foreach (var col in CurBoard.Columns)
                        GetColumnCards(col.Id);

                GetParticipants(CurBoard.Id);
            }
            
            AvaPath = ConfigurationManager.AppSettings["defaultAvaPath"];
        }

        void GetUser()
        {
            Task task;
            LoaderVisible = true;
            task = new Task(() =>
            {
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
            task.Start();
            task.Wait();
        }

        void GetBoards(string type)
        {
            LoaderVisible = true;
            BoardDTO[] boardsDTO = null;
            Task task = new Task(() =>
            {
                try
                {
                    boardsDTO = DataExchangeProxy.GetBoards(token, User.Id);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoaderVisible = false;
            });
            task.Start();
            task.Wait();

            if (boardsDTO != null)
            {
                switch (type)
                {
                    case nameof(User.PartBoards):
                        User.PartBoards = new ObservableCollection<BoardModel>
                        (Mapper.Map<BoardDTO[], BoardModel[]>(boardsDTO));
                        break;
                    case nameof(User.Boards):
                        User.Boards = new ObservableCollection<BoardModel>
                        (Mapper.Map<BoardDTO[], BoardModel[]>(boardsDTO));
                        break;
                    default:
                        break;
                }
            }
        }

        void GetColumns(int id)
        {
            LoaderVisible = true;
            ColumnDTO[] columnsDTO = null;
            Task task = new Task(() =>
            {
                try
                {
                    columnsDTO = DataExchangeProxy.GetColumns(token, id);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoaderVisible = false;
            });
            task.Start();
            task.Wait();

            if (columnsDTO != null)
            {
                CurBoard.Columns = new ObservableCollection<ColumnModel>
                (Mapper.Map<ColumnDTO[], ColumnModel[]>(columnsDTO));
            }
        }

        void GetColumnCards(int id)
        {
            LoaderVisible = true;
            CardDTO[] cardsDTO = null;
            Task task = new Task(() =>
            {
                try
                {
                    cardsDTO = DataExchangeProxy.GetCards(token, id);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoaderVisible = false;
            });
            task.Start();
            task.Wait();

            if (cardsDTO != null)
            {
                CurBoard.Columns.Where(x => x.Id == id).FirstOrDefault().Cards = 
                    new ObservableCollection<CardModel>
                (Mapper.Map<CardDTO[], CardModel[]>(cardsDTO));
            }
        }

        void GetParticipants(int id)
        {
            LoaderVisible = true;
            UserDTO[] partsDTO = null;
            Task task = new Task(() =>
            {
                try
                {
                    partsDTO = DataExchangeProxy.GetParticipants(token, id);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoaderVisible = false;
            });
            task.Start();
            task.Wait();

            if (partsDTO != null)
                CurBoard.Participants = new ObservableCollection<UserModel>(Mapper.Map<UserDTO[], UserModel[]>(partsDTO));
        }

        ////COMMANDS


    }
}
