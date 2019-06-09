using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IInteractionContract
    {
        [OperationContract]
        bool MoveCard(AuthenticationToken token, int cardId, int destinationColumnId);
    }
}
