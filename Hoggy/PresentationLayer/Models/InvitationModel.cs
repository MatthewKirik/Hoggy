using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class InvitationModel : ViewModelBase
    {
        public int Id { get; set; }

        
        private string _senderEmail;
        public string SenderEmail
        {
            get => _senderEmail;
            set
            {
                _senderEmail = value;
                RaisePropertyChanged(nameof(SenderEmail));
            }
        }

        public string Key { get; set; }

        public Action<InvitationModel> AcceptAct;
        public Action<InvitationModel> RejectAct;

        private RelayCommand _acceptCmd;
        public RelayCommand AcceptCmd
        {
            get
            {
                return _acceptCmd ?? (_acceptCmd = new RelayCommand(() =>
                {
                    if (AcceptAct != null)
                        AcceptAct(this);
                }));
            }
        }

        private RelayCommand _rejectCmd;
        public RelayCommand RejectCmd
        {
            get
            {
                return _rejectCmd ?? (_rejectCmd = new RelayCommand(() =>
                {
                    if (RejectAct != null)
                        RejectAct(this);
                }));
            }
        }
    }
}
