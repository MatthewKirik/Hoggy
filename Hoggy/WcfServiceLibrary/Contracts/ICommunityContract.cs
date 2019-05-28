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
    public interface ICommunityContract
    {
        [OperationContract]
        bool SendInvitation(AuthenticationToken token, int boardId, string email);
        [OperationContract]
        bool PostComment(AuthenticationToken token, CommentDTO comment, int cardId);
        [OperationContract]
        bool SubscribeToCard(AuthenticationToken token, CommentDTO comment, int cardId);
    }
}
