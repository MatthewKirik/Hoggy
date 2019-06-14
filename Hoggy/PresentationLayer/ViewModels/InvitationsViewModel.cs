using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class InvitationsViewModel : ViewModelBase
    {
        UserModel _user;
        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(nameof(User));
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

        public InvitationsViewModel(UserModel user)
        {
            _user = user;
            foreach (var invitation in User.Invitations)
            {
                invitation.AcceptAct = Accept;
                invitation.RejectAct = Reject;
            }
        }

        void Accept(InvitationModel invitation)
        {
            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    if (NetProxy.CommProxy.AcceptInvitation(NetProxy.Token, invitation.Key))
                    {
                        InvitationModel invit = User.Invitations.Where(i => i.Id == invitation.Id).FirstOrDefault();
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            if (invit != null)
                                User.Invitations.Remove(invit);
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

        void Reject(InvitationModel invitation)
        {
            MessageBox.Show("Reject!");
        }
}
}
