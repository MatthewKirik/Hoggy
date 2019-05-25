using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.ViewModels
{
    public class RegistrationWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// /ssdadasdasdasd
        /// </summary>
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
                CanSign = Validator.EmptyStrings(LoginErr,PassErr, MailErr);
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
                CanSign = Validator.EmptyStrings(LoginErr, PassErr, MailErr);
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
        public RegistrationWindowViewModel()
        {
            _loginErr = string.Empty;
            _passErr = string.Empty;
            _mailErr = string.Empty;
        }

        private RelayCommand _signUpCommand;
        public RelayCommand SignUpCommand
        {
            get
            {
                _signUpCommand = new RelayCommand(() =>
                {
                   
                });
                return _signUpCommand;
            }
        }
    }
}
