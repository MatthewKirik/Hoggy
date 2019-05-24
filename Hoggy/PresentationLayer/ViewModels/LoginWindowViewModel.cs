using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class LoginWindowViewModel : ViewModelBase
    {
        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        private RelayCommand _logInCommand;
        public RelayCommand LogInCommand
        {
            get
            {
                _logInCommand = new RelayCommand(() =>
                {
                });
                return _logInCommand;
            }
        }

    }
}
