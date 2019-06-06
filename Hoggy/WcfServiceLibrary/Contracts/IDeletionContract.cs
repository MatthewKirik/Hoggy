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
    public interface IDeletionContract
    {
        [OperationContract]
        bool DeleteBoard(AuthenticationToken token, int boardId);
        [OperationContract]
        bool DeleteBoardTag(AuthenticationToken token, int tagId);
        [OperationContract]
        bool DeleteColumn(AuthenticationToken token, int columnId);
        [OperationContract]
        bool DeleteCard(AuthenticationToken token, int cardId);
        [OperationContract]
        bool DeleteCardTag(AuthenticationToken token, int cardId, int tagId);
    }
}
