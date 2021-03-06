﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestConsoleClient.EditionService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EditionService.IEditionContract")]
    public interface IEditionContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditionContract/EditCard", ReplyAction="http://tempuri.org/IEditionContract/EditCardResponse")]
        bool EditCard(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CardDTO card);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditionContract/EditCard", ReplyAction="http://tempuri.org/IEditionContract/EditCardResponse")]
        System.Threading.Tasks.Task<bool> EditCardAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CardDTO card);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditionContract/EditColumn", ReplyAction="http://tempuri.org/IEditionContract/EditColumnResponse")]
        bool EditColumn(DataTransferObjects.AuthenticationToken token, DataTransferObjects.ColumnDTO column);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditionContract/EditColumn", ReplyAction="http://tempuri.org/IEditionContract/EditColumnResponse")]
        System.Threading.Tasks.Task<bool> EditColumnAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.ColumnDTO column);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditionContract/EditUserProfile", ReplyAction="http://tempuri.org/IEditionContract/EditUserProfileResponse")]
        bool EditUserProfile(DataTransferObjects.AuthenticationToken token, DataTransferObjects.UserProfileDTO userProfile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditionContract/EditUserProfile", ReplyAction="http://tempuri.org/IEditionContract/EditUserProfileResponse")]
        System.Threading.Tasks.Task<bool> EditUserProfileAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.UserProfileDTO userProfile);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEditionContractChannel : TestConsoleClient.EditionService.IEditionContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EditionContractClient : System.ServiceModel.ClientBase<TestConsoleClient.EditionService.IEditionContract>, TestConsoleClient.EditionService.IEditionContract {
        
        public EditionContractClient() {
        }
        
        public EditionContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EditionContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EditionContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EditionContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool EditCard(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CardDTO card) {
            return base.Channel.EditCard(token, card);
        }
        
        public System.Threading.Tasks.Task<bool> EditCardAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.CardDTO card) {
            return base.Channel.EditCardAsync(token, card);
        }
        
        public bool EditColumn(DataTransferObjects.AuthenticationToken token, DataTransferObjects.ColumnDTO column) {
            return base.Channel.EditColumn(token, column);
        }
        
        public System.Threading.Tasks.Task<bool> EditColumnAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.ColumnDTO column) {
            return base.Channel.EditColumnAsync(token, column);
        }
        
        public bool EditUserProfile(DataTransferObjects.AuthenticationToken token, DataTransferObjects.UserProfileDTO userProfile) {
            return base.Channel.EditUserProfile(token, userProfile);
        }
        
        public System.Threading.Tasks.Task<bool> EditUserProfileAsync(DataTransferObjects.AuthenticationToken token, DataTransferObjects.UserProfileDTO userProfile) {
            return base.Channel.EditUserProfileAsync(token, userProfile);
        }
    }
}
