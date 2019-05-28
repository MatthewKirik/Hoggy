﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestConsoleClient.DataExchangeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataExchangeService.IDataExchangeContract")]
    public interface IDataExchangeContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetUser", ReplyAction="http://tempuri.org/IDataExchangeContract/GetUserResponse")]
        DataTransferObjects.UserDTO GetUser(DataTransferObjects.AuthenticationToken token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetUser", ReplyAction="http://tempuri.org/IDataExchangeContract/GetUserResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.UserDTO> GetUserAsync(DataTransferObjects.AuthenticationToken token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetBoards", ReplyAction="http://tempuri.org/IDataExchangeContract/GetBoardsResponse")]
        DataTransferObjects.BoardDTO[] GetBoards(DataTransferObjects.AuthenticationToken token, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetBoards", ReplyAction="http://tempuri.org/IDataExchangeContract/GetBoardsResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.BoardDTO[]> GetBoardsAsync(DataTransferObjects.AuthenticationToken token, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetParticipatedBoards", ReplyAction="http://tempuri.org/IDataExchangeContract/GetParticipatedBoardsResponse")]
        DataTransferObjects.BoardDTO[] GetParticipatedBoards(DataTransferObjects.AuthenticationToken token, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetParticipatedBoards", ReplyAction="http://tempuri.org/IDataExchangeContract/GetParticipatedBoardsResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.BoardDTO[]> GetParticipatedBoardsAsync(DataTransferObjects.AuthenticationToken token, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetParticipants", ReplyAction="http://tempuri.org/IDataExchangeContract/GetParticipantsResponse")]
        DataTransferObjects.UserDTO[] GetParticipants(DataTransferObjects.AuthenticationToken token, int BoardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetParticipants", ReplyAction="http://tempuri.org/IDataExchangeContract/GetParticipantsResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.UserDTO[]> GetParticipantsAsync(DataTransferObjects.AuthenticationToken token, int BoardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetColumns", ReplyAction="http://tempuri.org/IDataExchangeContract/GetColumnsResponse")]
        DataTransferObjects.ColumnDTO[] GetColumns(DataTransferObjects.AuthenticationToken token, int BoardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetColumns", ReplyAction="http://tempuri.org/IDataExchangeContract/GetColumnsResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.ColumnDTO[]> GetColumnsAsync(DataTransferObjects.AuthenticationToken token, int BoardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetHistoryEvents", ReplyAction="http://tempuri.org/IDataExchangeContract/GetHistoryEventsResponse")]
        DataTransferObjects.HistoryEventDTO[] GetHistoryEvents(DataTransferObjects.AuthenticationToken token, int BoardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetHistoryEvents", ReplyAction="http://tempuri.org/IDataExchangeContract/GetHistoryEventsResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.HistoryEventDTO[]> GetHistoryEventsAsync(DataTransferObjects.AuthenticationToken token, int BoardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetCards", ReplyAction="http://tempuri.org/IDataExchangeContract/GetCardsResponse")]
        DataTransferObjects.CardDTO[] GetCards(DataTransferObjects.AuthenticationToken token, int ColumnId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetCards", ReplyAction="http://tempuri.org/IDataExchangeContract/GetCardsResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.CardDTO[]> GetCardsAsync(DataTransferObjects.AuthenticationToken token, int ColumnId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetCardSubscribers", ReplyAction="http://tempuri.org/IDataExchangeContract/GetCardSubscribersResponse")]
        DataTransferObjects.UserDTO[] GetCardSubscribers(DataTransferObjects.AuthenticationToken token, int CardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetCardSubscribers", ReplyAction="http://tempuri.org/IDataExchangeContract/GetCardSubscribersResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.UserDTO[]> GetCardSubscribersAsync(DataTransferObjects.AuthenticationToken token, int CardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetCardComments", ReplyAction="http://tempuri.org/IDataExchangeContract/GetCardCommentsResponse")]
        DataTransferObjects.CommentDTO[] GetCardComments(DataTransferObjects.AuthenticationToken token, int CardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchangeContract/GetCardComments", ReplyAction="http://tempuri.org/IDataExchangeContract/GetCardCommentsResponse")]
        System.Threading.Tasks.Task<DataTransferObjects.CommentDTO[]> GetCardCommentsAsync(DataTransferObjects.AuthenticationToken token, int CardId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataExchangeContractChannel : TestConsoleClient.DataExchangeService.IDataExchangeContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataExchangeContractClient : System.ServiceModel.ClientBase<TestConsoleClient.DataExchangeService.IDataExchangeContract>, TestConsoleClient.DataExchangeService.IDataExchangeContract {
        
        public DataExchangeContractClient() {
        }
        
        public DataExchangeContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataExchangeContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataExchangeContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataExchangeContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public DataTransferObjects.UserDTO GetUser(DataTransferObjects.AuthenticationToken token) {
            return base.Channel.GetUser(token);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.UserDTO> GetUserAsync(DataTransferObjects.AuthenticationToken token) {
            return base.Channel.GetUserAsync(token);
        }
        
        public DataTransferObjects.BoardDTO[] GetBoards(DataTransferObjects.AuthenticationToken token, int UserId) {
            return base.Channel.GetBoards(token, UserId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.BoardDTO[]> GetBoardsAsync(DataTransferObjects.AuthenticationToken token, int UserId) {
            return base.Channel.GetBoardsAsync(token, UserId);
        }
        
        public DataTransferObjects.BoardDTO[] GetParticipatedBoards(DataTransferObjects.AuthenticationToken token, int UserId) {
            return base.Channel.GetParticipatedBoards(token, UserId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.BoardDTO[]> GetParticipatedBoardsAsync(DataTransferObjects.AuthenticationToken token, int UserId) {
            return base.Channel.GetParticipatedBoardsAsync(token, UserId);
        }
        
        public DataTransferObjects.UserDTO[] GetParticipants(DataTransferObjects.AuthenticationToken token, int BoardId) {
            return base.Channel.GetParticipants(token, BoardId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.UserDTO[]> GetParticipantsAsync(DataTransferObjects.AuthenticationToken token, int BoardId) {
            return base.Channel.GetParticipantsAsync(token, BoardId);
        }
        
        public DataTransferObjects.ColumnDTO[] GetColumns(DataTransferObjects.AuthenticationToken token, int BoardId) {
            return base.Channel.GetColumns(token, BoardId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.ColumnDTO[]> GetColumnsAsync(DataTransferObjects.AuthenticationToken token, int BoardId) {
            return base.Channel.GetColumnsAsync(token, BoardId);
        }
        
        public DataTransferObjects.HistoryEventDTO[] GetHistoryEvents(DataTransferObjects.AuthenticationToken token, int BoardId) {
            return base.Channel.GetHistoryEvents(token, BoardId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.HistoryEventDTO[]> GetHistoryEventsAsync(DataTransferObjects.AuthenticationToken token, int BoardId) {
            return base.Channel.GetHistoryEventsAsync(token, BoardId);
        }
        
        public DataTransferObjects.CardDTO[] GetCards(DataTransferObjects.AuthenticationToken token, int ColumnId) {
            return base.Channel.GetCards(token, ColumnId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.CardDTO[]> GetCardsAsync(DataTransferObjects.AuthenticationToken token, int ColumnId) {
            return base.Channel.GetCardsAsync(token, ColumnId);
        }
        
        public DataTransferObjects.UserDTO[] GetCardSubscribers(DataTransferObjects.AuthenticationToken token, int CardId) {
            return base.Channel.GetCardSubscribers(token, CardId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.UserDTO[]> GetCardSubscribersAsync(DataTransferObjects.AuthenticationToken token, int CardId) {
            return base.Channel.GetCardSubscribersAsync(token, CardId);
        }
        
        public DataTransferObjects.CommentDTO[] GetCardComments(DataTransferObjects.AuthenticationToken token, int CardId) {
            return base.Channel.GetCardComments(token, CardId);
        }
        
        public System.Threading.Tasks.Task<DataTransferObjects.CommentDTO[]> GetCardCommentsAsync(DataTransferObjects.AuthenticationToken token, int CardId) {
            return base.Channel.GetCardCommentsAsync(token, CardId);
        }
    }
}