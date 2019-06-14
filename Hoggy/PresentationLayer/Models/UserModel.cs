using DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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
                LoginErr = Validator.Check<int>(CheckType.Empty, Login);
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
                MailErr = Validator.Check<int>(CheckType.Mail, Email);
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
                CanLogin = Validator.EmptyStrings(MailErr, PassErr);
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

        string _avatar;
        public string Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                RaisePropertyChanged(nameof(Avatar));
            }
        }

        public ObservableCollection<BoardModel> Boards { get; set; }
        public ObservableCollection<BoardModel> PartBoards { get; set; }
        public ObservableCollection<InvitationModel> Invitations { get; set; }

        public UserModel()
        {
            _loginErr = "Empty field";
            _passErr = "Empty field";
            _mailErr = "Empty field";
            _avatar = ConfigurationManager.AppSettings["defaultAvaPath"];
            Boards = new ObservableCollection<BoardModel>();
            PartBoards = new ObservableCollection<BoardModel>();
            Invitations = new ObservableCollection<InvitationModel>();
        }

        public Action<UserModel> AddTagToCardAct;

        //COMMANDS
        private RelayCommand _inviteUserCmd;
        public RelayCommand InviteUserCmd
        {
            get
            {
                return _inviteUserCmd ?? (_inviteUserCmd = new RelayCommand(() =>
                {
                    AddTagToCardAct.Invoke(this);
                }));
            }
        }

    }
}
