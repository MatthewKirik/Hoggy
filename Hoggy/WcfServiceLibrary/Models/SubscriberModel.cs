using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Contracts;

namespace WcfServiceLibrary.Models
{
    public class SubscriberModel
    {
        public INotifiactionCallbackContract Callback { get; set; }
        public int SecurityGroupId { get; set; }
        public string Email { get; set; }
    }
}
