using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Models
{
    public class UserModel : ViewModelBase , IDataErrorInfo
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

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<BoardModel> Boards { get; set; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Login":
                        if (Login.Length < 3)
                        {
                            error = "Login to short!";
                            break;
                        }
                        break;
                    case "Password":
                        if (Password.Length < 8)
                        {
                            error = "Password to short!";
                            break;
                        }
                        break;
                    case "Email":
                        if (Email.Length == 0)
                        {
                            error = "Empty field!";
                            break;
                        }
                        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        Match match = regex.Match(Email);
                        if (!match.Success)
                        error = "Email is not correct!";
                        break;
                }
                return error;
            }
        }
    }
}
