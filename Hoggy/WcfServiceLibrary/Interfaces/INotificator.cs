using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Contracts;
using WcfServiceLibrary.Models;

namespace WcfServiceLibrary.Interfaces
{
    public interface INotificator : INotifiactionCallbackContract
    {
        List<SubscriberModel> Subscribers { get; }
        INotificator WithSecurityGroup(int id);
    }
}
