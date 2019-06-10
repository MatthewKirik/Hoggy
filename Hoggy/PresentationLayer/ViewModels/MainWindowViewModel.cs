using AutoMapper;
using DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.AuthenticationService;
using PresentationLayer.DataExchangeService;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using PresentationLayer.NotificationService;
using PresentationLayer.RegistrationService;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        //PROPERTIES
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

        bool _openExpander;
        public bool OpenExpander
        {
            get => _openExpander;
            set
            {
                _openExpander = value;
                RaisePropertyChanged(nameof(OpenExpander));
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
        Window _mainWindow;

        public MainWindowViewModel(Window mainWindow)
        {
            NetProxy.Configure();
            NetProxy.CallbackHandler.AddNewCardHandler(AddNewCard); 

            _mainWindow = mainWindow;
            MapperConfigurator.Configure();

            RegistrationWindow auth = new RegistrationWindow();
            if (auth.ShowDialog() != true)
                _mainWindow.Close();
            else
            {
                RegistrationWindowViewModel regModel = (RegistrationWindowViewModel)auth.DataContext;

                NetProxy.SetToken(regModel.Token);
                
                User = new UserModel();

                GetUser();
                GetBoards(nameof(User.Boards));
                GetBoards(nameof(User.PartBoards));

                if (User.Boards.Count + User.PartBoards.Count > 0)
                    CurBoard = (User.Boards.Count > 0) ? User.Boards[0] : User.PartBoards[0];

                LoadCurrentBoard();
                AvaPath = ConfigurationManager.AppSettings["defaultAvaPath"];
            }
        }
        
        void AddNewCard(CardModel card, int colId)
        {
            ColumnModel col = CurBoard.Columns.Where(c => c.Id == colId).FirstOrDefault();
            if (col != null)
                col.Cards.Add(card);
        }

        void GetUser()
        {
            Task task;
            LoaderVisible = true;
            task = new Task(() =>
            {
                try
                {
                    UserDTO userDTO = NetProxy.DataExchProxy.GetUser(NetProxy.Token);
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
                    boardsDTO = NetProxy.DataExchProxy.GetBoards(NetProxy.Token, User.Id);
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
                        foreach (var board in User.PartBoards)
                            board.ChangeBoard = ChangeCurrentBoard;
                        break;
                    case nameof(User.Boards):
                        User.Boards = new ObservableCollection<BoardModel>
                        (Mapper.Map<BoardDTO[], BoardModel[]>(boardsDTO));
                        foreach (var board in User.Boards)
                            board.ChangeBoard = ChangeCurrentBoard;
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
                    columnsDTO = NetProxy.DataExchProxy.GetColumns(NetProxy.Token, id);
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
                CurBoard.Columns.Clear();
                foreach (var col in columnsDTO)
                {
                    CurBoard.Columns.Add(Mapper.Map<ColumnModel>(col));
                }
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
                    cardsDTO = NetProxy.DataExchProxy.GetCards(NetProxy.Token, id);
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
                ColumnModel col = CurBoard.Columns.Where(x => x.Id == id).FirstOrDefault();
                if (col == null)
                    return;
                col.Cards.Clear();
                foreach (var card in cardsDTO)
                {
                    col.Cards.Add(Mapper.Map<CardModel>(card));
                }
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
                    partsDTO = NetProxy.DataExchProxy.GetParticipants(NetProxy.Token, id);
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

        void ChangeCurrentBoard(int id)
        {
            OpenExpander = false;
            if (id == CurBoard.Id)
                return;

            BoardModel board = User.Boards.Where(x => x.Id == id).FirstOrDefault();
            if (board == null)
                board = User.PartBoards.Where(x => x.Id == id).FirstOrDefault();

            if (board == null)
                return;

            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    //_notificationProxy.UNSUBSCRIBE(token, CurBoard.Id);
                    Thread.Sleep(3000);
                    CurBoard = board;
                    LoadCurrentBoard();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    LoaderVisible = false;
                }
            });
        }

        void LoadCurrentBoard()
        {
            if (CurBoard == null)
                return;

            LoaderVisible = true;
            Task.Run(() =>
            {
                bool isSubscribe = false;
                try
                {
                    //NetProxy.Configure();
                    isSubscribe = NetProxy.NotificationProxy.Subscribe(NetProxy.Token, CurBoard.Id);
                    ColumnDTO[] columnsDTO = NetProxy.DataExchProxy.GetColumns(NetProxy.Token, CurBoard.Id);
                    if (columnsDTO == null || columnsDTO.Length == 0)
                        return;

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        CurBoard.Columns.Clear();
                        foreach (var col in columnsDTO)
                        {
                            ColumnModel columnModel = Mapper.Map<ColumnModel>(col);
                            columnModel.MoveCardAct = MoveCard;
                            CurBoard.Columns.Add(columnModel);
                        }
                    });

                    foreach (var col in CurBoard.Columns)
                    {
                        CardDTO[] cardsDTO = NetProxy.DataExchProxy.GetCards(NetProxy.Token, col.Id);
                        if (cardsDTO != null && cardsDTO.Length > 0)
                        {
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                col.Cards.Clear();
                                foreach (var card in cardsDTO)
                                    col.Cards.Add(Mapper.Map<CardModel>(card));
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!",
                         MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    LoaderVisible = false;
                }
            });

        }

        void MoveCard(int cardId, int colId, int position)
        {
            MessageBox.Show(cardId + " " + colId + " " + position);

        }

        ////COMMANDS
    }
}
