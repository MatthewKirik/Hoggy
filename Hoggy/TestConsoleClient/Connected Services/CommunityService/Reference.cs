﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestConsoleClient.CommunityService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CommunityService.ICommunityContract")]
    public interface ICommunityContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommunityContract/SendInvitation", ReplyAction="http://tempuri.org/ICommunityContract/SendInvitationResponse")]
        bool SendInvitation(DataTransferObjects.AuthenticationToken token, int boardId, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommunityContract/SendInvitation", ReplyAction="http://tempuri.org/ICommunityContract/SendInvitationResponse")]
        System.Threading.Tasks.Task<bool> SendInvitationAsync(DataTransferObjects.AuthenticationToken token, int boardId, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommunityContract/PostComment", ReplyAction="http://tempuri.org/ICommunityContract/PostCommentResponse")]
        bool PostComment(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommunityContract/PostComment", ReplyAction="http://tempuri.org/ICommunityContract/PostCommentResponse")]
        System.Threading.Tasks.Task<bool> PostCommentAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommunityContract/SubscribeToCard", ReplyAction="http://tempuri.org/ICommunityContract/SubscribeToCardResponse")]
        bool SubscribeToCard(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommunityContract/SubscribeToCard", ReplyAction="http://tempuri.org/ICommunityContract/SubscribeToCardResponse")]
        System.Threading.Tasks.Task<bool> SubscribeToCardAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICommunityContractChannel : TestConsoleClient.CommunityService.ICommunityContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CommunityContractClient : System.ServiceModel.ClientBase<TestConsoleClient.CommunityService.ICommunityContract>, TestConsoleClient.CommunityService.ICommunityContract {
        
        public CommunityContractClient() {
        }
        
        public CommunityContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CommunityContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommunityContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommunityContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool SendInvitation(DataTransferObjects.AuthenticationToken token, int boardId, string email) {
            return base.Channel.SendInvitation(token, boardId, email);
        }
        
        public System.Threading.Tasks.Task<bool> SendInvitationAsync(DataTransferObjects.AuthenticationToken token, int boardId, string email) {
            return base.Channel.SendInvitationAsync(token, boardId, email);
        }
        
        public bool PostComment(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId) {
            return base.Channel.PostComment(token, comment, cardId);
        }
        
        public System.Threading.Tasks.Task<bool> PostCommentAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId) {
            return base.Channel.PostCommentAsync(token, comment, cardId);
        }
        
        public bool SubscribeToCard(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId) {
            return base.Channel.SubscribeToCard(token, comment, cardId);
        }
        
        public System.Threading.Tasks.Task<bool> SubscribeToCardAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CommentDTO comment, int cardId) {
            return base.Channel.SubscribeToCardAsync(token, comment, cardId);
        }
    }
}
