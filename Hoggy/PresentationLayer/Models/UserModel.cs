using DataTransferObjects;
using GalaSoft.MvvmLight;
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
        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
            }
        }

        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        public ObservableCollection<BoardModel> Boards { get; set; }
    }
}
