using GalaSoft.MvvmLight;
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
        public string SenderEmail { get; set; }
        public string Key { get; set; }
    }
}
