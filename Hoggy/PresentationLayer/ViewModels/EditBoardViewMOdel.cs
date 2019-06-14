using AutoMapper;
using DataTransferObjects;
using GalaSoft.MvvmLight;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class EditBoardViewModel : ViewModelBase
    {
        BoardModel _board;
        public BoardModel Board
        {
            get => _board;
            set
            {
                _board = value;
                RaisePropertyChanged(nameof(Board));
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

        public ObservableCollection<UserModel> Users { get; set; }

        Window _window;
        public EditBoardViewModel(BoardModel board, Window window)
        {
            Users = new ObservableCollection<UserModel>();
            _board = (BoardModel)board.Clone();
            _window = window;
            LoadAllUsers();
        }

        void LoadAllUsers()
        {
            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    UserDTO[] usersDTO = NetProxy.DataExchProxy.GetAllUsers(NetProxy.Token);
                    if (usersDTO != null && usersDTO.Length > 0)
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            Users.Clear();
                            foreach (var userDTO in usersDTO)
                            {
                                UserModel userModel = Mapper.Map<UserModel>(userDTO);
                                userModel.AddTagToCardAct = AddParticipants;
                                Users.Add(userModel);
                            }
                        });
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

        void AddParticipants(UserModel user)
        {
            if (Board.Participants.Where(u => u.Id == user.Id).FirstOrDefault() != null)
                return;
            else
            {
                LoaderVisible = true;
                Task.Run(() =>
                {
                    try
                    {
                        if (!NetProxy.CommProxy.SendInvitation(NetProxy.Token, Board.Id, user.Email))
                            MessageBox.Show("Invitattion is not send!");
                        else
                            MessageBox.Show("Invitattion is send!");
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
        }
    }
}