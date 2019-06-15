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
    public class MainWindowViewModel : ViewModelBase
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

        bool isSubscribe;

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

        TagModel _curTag;
        public TagModel CurTag
        {
            get => _curTag;
            set
            {
                _curTag = value;
                RaisePropertyChanged(nameof(CurTag));
            }
        }

        Window _mainWindow;
        public MainWindowViewModel(Window mainWindow)
        {
            NetProxy.Configure();
            NetProxy.CallbackHandler.AddNewCardHandler(AddNewCard);
            NetProxy.CallbackHandler.AddMoveCardHandler(MoveCardCallback);
            NetProxy.CallbackHandler.AddEditCardHandler(EditCardCallback);
            NetProxy.CallbackHandler.AddNewTagToBoardHandler(AddTagToBoardCallback);
            NetProxy.CallbackHandler.AddNewColumnHandler(AddColumnCallback);
            NetProxy.CallbackHandler.AddDelColumnHandler(DeleteColumnCallback);
            NetProxy.CallbackHandler.AddEditColumnHandler(EditColumnCallback);
            NetProxy.CallbackHandler.AddCardTagAddedHandler(AddTagToCard);
            NetProxy.CallbackHandler.AddOnIncomeInvitationHandler(OnIncomeInvitationCallback);
            NetProxy.CallbackHandler.AddOnHistoryEventAddedHandler(OnHistoryEventAddedCallback);
            NetProxy.CallbackHandler.AddOnBoardAddedHandler(OnBoardAddedCallback);

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
                if (User.Boards.Count + User.PartBoards.Count > 0)
                    CurBoard = (User.Boards.Count > 0) ? User.Boards[0] : User.PartBoards[0];
                LoadCurrentBoard();
                AvaPath = ConfigurationManager.AppSettings["defaultAvaPath"];
                CurTag = new TagModel();
            }
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
                        GetInvitations();
                        GetBoards(nameof(User.Boards));
                        GetBoards(nameof(User.PartBoards));
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

        void GetInvitations()
        {
            try
            {
                InvitationDTO[] invitationsDTO = NetProxy.DataExchProxy.GetIncomeInvitations(
                    NetProxy.Token, User.Id);
                if (invitationsDTO != null)
                {
                    User.Invitations.Clear();
                    foreach (var invitationDTO in invitationsDTO)
                        User.Invitations.Add(Mapper.Map<InvitationModel>(invitationDTO));
                }
                else
                    MessageBox.Show("Cant load invitations");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void GetBoards(string type)
        {
            LoaderVisible = true;
            BoardDTO[] boardsDTO = null;
            Task task = new Task(() =>
            {
                try
                {
                    boardsDTO = (type == nameof(User.PartBoards)) ?
                    NetProxy.DataExchProxy.GetParticipatedBoards(NetProxy.Token, User.Id) :
                    NetProxy.DataExchProxy.GetBoards(NetProxy.Token, User.Id);

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

        void GetParticipants()
        {
            try
            {
                UserDTO[] partsDTO = NetProxy.DataExchProxy.GetParticipants(NetProxy.Token, CurBoard.Id);
                if (partsDTO != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        CurBoard.Participants.Clear();
                        foreach (var part in partsDTO)
                            CurBoard.Participants.Add(Mapper.Map<UserModel>(part));
                    }); 
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void GetEvents()
        {
            try
            {
                HistoryEventDTO[] eventsDTO = NetProxy.DataExchProxy.GetHistoryEvents(NetProxy.Token, CurBoard.Id);
                if (eventsDTO != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        CurBoard.HistoryEvents.Clear();
                        foreach (var eventDTO in eventsDTO)
                            CurBoard.HistoryEvents.Add(Mapper.Map<HistoryEventModel>(eventDTO));
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                    if (isSubscribe)
                        NetProxy.NotificationProxy.UnSubscribe(NetProxy.Token, CurBoard.Id);
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
                            columnModel.MainWindow = _mainWindow;
                            CurBoard.Columns.Add(columnModel);
                        }
                    });
                    //load tags
                    TagDTO[] allTags = NetProxy.DataExchProxy.GetBoardTags(NetProxy.Token, CurBoard.Id);
                    if (allTags != null)
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            CurBoard.Tags.Clear();
                            foreach (var tag in allTags)
                                CurBoard.Tags.Add(Mapper.Map<TagModel>(tag));
                        });
                    //participants and events
                    GetParticipants();
                    GetEvents();
                    //load cards
                    foreach (var col in CurBoard.Columns)
                    {
                        CardDTO[] cardsDTO = NetProxy.DataExchProxy.GetCards(NetProxy.Token, col.Id);
                        if (cardsDTO != null && cardsDTO.Length > 0)
                        {
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                col.Cards.Clear();
                                foreach (var card in cardsDTO)
                                {
                                    CardModel cardModel = Mapper.Map<CardModel>(card);
                                    cardModel.ColumnId = col.Id;
                                    cardModel.BoardId = CurBoard.Id;
                                    cardModel.BoardTags = CurBoard.Tags;
                                    col.Cards.Add(cardModel);
                                }
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

        void MoveCard(int cardId, int originalColumnId, int destinationColumnId)
        {
            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    NetProxy.InterProxy.MoveCard(NetProxy.Token, cardId, destinationColumnId);
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

        //CALLBACKS
        void AddNewCard(CardDTO card, int colId)
        {
            ColumnModel col = CurBoard.Columns.Where(c => c.Id == colId).FirstOrDefault();
            if (col != null)
            {
                CardModel cardModel = Mapper.Map<CardModel>(card);
                cardModel.ColumnId = colId;
                cardModel.BoardId = CurBoard.Id;
                cardModel.BoardTags = CurBoard.Tags;
                col.Cards.Add(cardModel);
            }
        }

        void MoveCardCallback(int cardId, int originalColumnId, int destinationColumnId)
        {
            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    ColumnModel col = CurBoard.Columns.Where(c => c.Id == originalColumnId).FirstOrDefault();
                    ColumnModel destcol = CurBoard.Columns.Where(c => c.Id == destinationColumnId).FirstOrDefault();
                    CardModel card = col.Cards.Where(c => c.Id == cardId).FirstOrDefault();

                    if (col == null || destcol == null || card == null)
                        return;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        col.Cards.Remove(card);
                        destcol.Cards.Add(card);
                    });
                    card.ColumnId = destcol.Id;
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

        void EditCardCallback(CardDTO cardDTO)
        {
            CardModel cardModel = null;
            foreach (var col in CurBoard.Columns)
            {
                cardModel = col.Cards.Where(c => c.Id == cardDTO.Id).FirstOrDefault();
                if (cardModel != null)
                    break;
            }
            if (cardModel == null)
                return;

            cardModel.Name = cardDTO.Name;
            cardModel.Description = cardDTO.Description;
            cardModel.ExpireDate = cardDTO.ExpireDate;
        }

        void AddTagToBoardCallback(TagDTO tag)
        {
            if (tag != null)
                CurBoard.Tags.Add(new TagModel
                {
                    Name = tag.Name,
                    Color = (Color)ColorConverter.ConvertFromString(tag.Color)
                });
        }

        void AddTagToCard(int tagId, int cardId)
        {
            CardModel cardModel = null;
            foreach (var col in CurBoard.Columns)
            {
                cardModel = col.Cards.Where(c => c.Id == cardId).FirstOrDefault();
                if (cardModel != null)
                    break;
            }
            if (cardModel == null)
                return;

            TagModel tag = CurBoard.Tags.Where(t => t.Id == tagId).FirstOrDefault();
            if (tag != null)
                cardModel.Tags.Add(tag);
        }

        void AddColumnCallback(ColumnDTO columnDTO)
        {
            CurBoard.Columns.Add(Mapper.Map<ColumnModel>(columnDTO));
        }

        void EditColumnCallback(ColumnDTO col)
        {
            ColumnModel colModel = CurBoard.Columns.Where(c => c.Id == col.Id).FirstOrDefault();
            if (colModel != null)
            {
                colModel.Name = col.Name;
                colModel.Description = col.Description;
            }
        }

        void DeleteColumnCallback(int colId)
        {
            ColumnModel col = CurBoard.Columns.Where(c => c.Id == colId).FirstOrDefault();
            if (col != null)
                CurBoard.Columns.Remove(col);
        }

        void OnIncomeInvitationCallback(InvitationDTO invitation, string recepientEmail)
        {
            User.Invitations.Add(Mapper.Map<InvitationModel>(invitation));
            MessageBox.Show("The user " + invitation.SenderEmail + " send invitation for you! \n Check your invitations.",
                "New invitation!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        void OnHistoryEventAddedCallback(HistoryEventDTO historyEvent)
        {
            HistoryEventModel historyEventModel = Mapper.Map<HistoryEventModel>(historyEvent);
            CurBoard.HistoryEvents.Add(historyEventModel);
        }

        void OnBoardAddedCallback(BoardDTO boardDTO)
        {
            BoardModel boardModel = Mapper.Map<BoardModel>(boardDTO);
            if (boardModel == null)
                return;
            User.Boards.Add(boardModel);
            //ChangeCurrentBoard(boardModel.Id);
        }

        ////COMMANDS
        private RelayCommand _newTagCmd;
        public RelayCommand NewTagCmd
        {
            get
            {
                return _newTagCmd ?? (_newTagCmd = new RelayCommand(() =>
                {
                    LoaderVisible = true;
                    Task.Run(() =>
                    {
                        try
                        {
                            if (!NetProxy.CreationProxy.AddTagToBoard(NetProxy.Token, new TagDTO
                            {
                                Name = CurTag.Name,
                                Color = CurTag.Color.ToString()
                            }, CurBoard.Id))
                                MessageBox.Show("Can't add tag!");
                            else
                                CurTag = new TagModel();
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
                }));
            }
        }

        private RelayCommand _addColumnCmd;
        public RelayCommand AddColumnCmd
        {
            get
            {
                return _addColumnCmd ?? (_addColumnCmd = new RelayCommand(() =>
                {
                    AddEditColumnWindow addColWind = new AddEditColumnWindow(CurBoard.Id);
                    addColWind.ShowDialog();
                }));
            }
        }

        private RelayCommand _editBoardCmd;
        public RelayCommand EditBoardCmd
        {
            get
            {
                return _editBoardCmd ?? (_editBoardCmd = new RelayCommand(() =>
                {
                    EditBoardWindow editBoardWindow = new EditBoardWindow(CurBoard);
                    editBoardWindow.ShowDialog();
                }));
            }
        }

        private RelayCommand _createBoardCmd;
        public RelayCommand CreateBoardCmd
        {
            get
            {
                return _createBoardCmd ?? (_createBoardCmd = new RelayCommand(() =>
                {
                    AddBoardWindow addBoardWindow = new AddBoardWindow();
                    addBoardWindow.ShowDialog();
                }));
            }
        }

        private RelayCommand _invitationsCmd;
        public RelayCommand InvitationsCmd
        {
            get
            {
                return _invitationsCmd ?? (_invitationsCmd = new RelayCommand(() =>
                {
                    InvitationsWindow invitationsWindow = new InvitationsWindow(User);
                    invitationsWindow.ShowDialog();
                }));
            }
        }
    }
}
