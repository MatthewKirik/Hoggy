using DataTransferObjects;
using System.ServiceModel;

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
        [OperationContract]
        bool AddTagToCard(AuthenticationToken token, int tagId, int cardId);
    }
}
