using DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.AuthenticationService;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using PresentationLayer.RegistrationService;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PresentationLayer.ViewModels
{
    public class RegistrationWindowViewModel : ViewModelBase
    {
        Window _window;
        UserModel _user;
        public UserModel User { get => _user; }
        public AuthenticationToken Token { get; set; }

        public RegistrationWindowViewModel(Window window)
        {
            _window = window;
            _user = new UserModel();


            ///LOCAL TEST
            _user.Email = "user1@gmail.com";
            _user.Password = "user1";
            //LoaderVisible = true;
            //Task.Run(() =>
            //{
            //    try
            //    {
            //        Token = NetProxy.AuthProxy.Login(User.Email, User.Password);
            //        if (Token != null)
            //        {
            //            _window.Dispatcher.Invoke(() =>
            //            {
            //                _window.DialogResult = true;
            //                _window.Close();
            //            });
            //        }
            //        else
            //            MessageBox.Show(User.Login + User.Password + "Wrong login or password! Try again.");
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //    LoaderVisible = false;
            //});
        }

        private bool _loaderVisible;
        public bool LoaderVisible
        {
            get => _loaderVisible;
            set
            {
                _loaderVisible = value;
                RaisePropertyChanged(nameof(LoaderVisible));
            }
        }

        private RelayCommand _signUpCmd;
        public RelayCommand SignUpCmd
        {
            get
            {
                return _signUpCmd ?? (_signUpCmd = new RelayCommand(() =>
                {
                    LoaderVisible = true;
                    Task.Run(() => {
                        try
                        {
                            if (NetProxy.RegProxy.RegisterUser(new UserDTO { Login = User.Login, Email = User.Email }, User.Password))
                            {
                                Token = NetProxy.AuthProxy.Login(User.Email, User.Password);
                                _window.Dispatcher.Invoke(() =>
                                {
                                    _window.DialogResult = true;
                                    _window.Close();
                                });
                            }
                            else
                                MessageBox.Show("User is alredy exist!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        LoaderVisible = false;
                    });
                }));
            }
        }

        //COMMANDS
        private RelayCommand _loginCmd;
        public RelayCommand LoginCmd
        {
            get
            {
                return _loginCmd ?? (_loginCmd = new RelayCommand(() =>
                {
                    LoaderVisible = true;
                    Task.Run(() => {
                        try
                        {
                            Token = NetProxy.AuthProxy.Login(User.Email, User.Password);
                            if (Token != null)
                            {
                                _window.Dispatcher.Invoke(() =>
                                {
                                    _window.DialogResult = true;
                                    _window.Close();
                                });
                            }
                            else
                                MessageBox.Show(User.Login + User.Password + "Wrong login or password! Try again.");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        LoaderVisible = false;
                    });
                }));
            }
        }

        private RelayCommand _cancelCmd;
        public RelayCommand CancelCmd
        {
            get
            {
                return _cancelCmd ?? (_cancelCmd = new RelayCommand(() =>
                {
                    _window.DialogResult = false;
                    _window.Close();
                }));
            }
        }
    }
}
