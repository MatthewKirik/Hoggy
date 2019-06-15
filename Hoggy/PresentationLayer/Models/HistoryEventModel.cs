using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class HistoryEventModel : ViewModelBase
    {
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                RaisePropertyChanged(nameof(Text));
            }
        }

        private string _producerLogin;
        public string ProducerLogin
        {
            get => _producerLogin;
            set
            {
                _producerLogin = value;
                RaisePropertyChanged(nameof(ProducerLogin));
            }
        }

    }
}