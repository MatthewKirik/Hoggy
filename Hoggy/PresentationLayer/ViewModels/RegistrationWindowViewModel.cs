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
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged(nameof(Login));
                LoginErr = Validator.Check(CheckType.Empty, Login);
            }
        }

        private string _loginErr;
        public string LoginErr
        {
            get => _loginErr;
            set
            {
                _loginErr = value;
                RaisePropertyChanged(nameof(LoginErr));
                CanSign = Validator.EmptyStrings(LoginErr, PassErr, MailErr);
                CanLogin = Validator.EmptyStrings(MailErr, PassErr);
            }
        }

        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
                PassErr = Validator.Check(CheckType.LengthMoreThan, Password, 8);
            }
        }
        private string _passErr;
        public string PassErr
        {
            get => _passErr;
            set
            {
                _passErr = value;
                RaisePropertyChanged(nameof(PassErr));
                CanSign = Validator.EmptyStrings(LoginErr, PassErr, MailErr);
                CanLogin = Validator.EmptyStrings(MailErr, PassErr);
            }
        }

        string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(nameof(Email));
                MailErr = Validator.Check(CheckType.Mail, Email);
            }
        }
        private string _mailErr;
        public string MailErr
        {
            get => _mailErr;
            set
            {
                _mailErr = value;
                RaisePropertyChanged(nameof(MailErr));
                CanSign = Validator.EmptyStrings(MailErr, PassErr, MailErr);
            }
        }

        bool _canSign;
        public bool CanSign
        {
            set
            {
                _canSign = value;
                RaisePropertyChanged(nameof(CanSign));
            }
            get => _canSign;
        }

        bool _canLogin;
        public bool CanLogin
        {
            set
            {
                _canLogin = value;
                RaisePropertyChanged(nameof(CanLogin));
            }
            get => _canLogin;
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

        Window _window;
        public RegistrationContractClient RegProxy { get; }
        public AuthenticationContractClient AuthProxy {  get; }
        UserModel _user;
        public UserModel User { get => _user; }
        public AuthenticationToken Token { get; set; }

        public RegistrationWindowViewModel(Window window)
        {
            _loginErr = "Empty field";
            _passErr = "Empty field";
            _mailErr = "Empty field";
            RegProxy = new RegistrationContractClient();
            AuthProxy = new AuthenticationContractClient();
            _window = window;
            _user = new UserModel();
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
                        _user.Login = Login;
                        _user.Email = Email;
                        _user.Password = Password;
                        try
                        {
                            if (RegProxy.RegisterUser(new UserDTO { Login = User.Login, Email = User.Email }, User.Password))
                            {
                                Token = AuthProxy.Login(User.Email, User.Password);

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

        private RelayCommand _loginCmd;
        public RelayCommand LoginCmd
        {
            get
            {
                return _loginCmd ?? (_loginCmd = new RelayCommand(() =>
                {
                    LoaderVisible = true;
                    Task.Run(() => {
                        _user.Email = Email;
                        _user.Password = Password;
                        try
                        {
                            Token = AuthProxy.Login(User.Email, User.Password);
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
