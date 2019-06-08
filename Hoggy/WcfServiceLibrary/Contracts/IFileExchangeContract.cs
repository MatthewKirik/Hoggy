using DataTransferObjects;
using DataTransferObjects.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IFileExchangeContract
    {
        [OperationContract]
        GetImageResponseMessage GetUserProfileImage(GetImageRequestMessage request);
        [OperationContract(IsOneWay = true)]
        void SetUserProfileImage(AddImageRequestMessage request);
    }
}
