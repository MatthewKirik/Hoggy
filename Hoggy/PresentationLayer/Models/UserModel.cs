using DataTransferObjects;
using GalaSoft.MvvmLight;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Models
{
    public class UserModel : ViewModelBase
    {
        public int Id { get; set; }

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
                PassErr = Validator.Check(CheckType.LengthMoreThan, Password, 3);
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
        
        public ObservableCollection<BoardModel> Boards { get; set; }
        
        public UserModel()
        {
            _loginErr = "Empty field";
            _passErr = "Empty field";
            _mailErr = "Empty field";
        }

    }
}
