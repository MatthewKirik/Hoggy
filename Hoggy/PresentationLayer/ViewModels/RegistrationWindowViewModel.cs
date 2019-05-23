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
    public class RegistrationWindowViewModel : ViewModelBase
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
