﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestConsoleClient.FileExchangeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FileExchangeService.IFileExchangeContract")]
    public interface IFileExchangeContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileExchangeContract/GetUserProfileImage", ReplyAction="http://tempuri.org/IFileExchangeContract/GetUserProfileImageResponse")]
        TestConsoleClient.FileExchangeService.GetImageResponseMessage GetUserProfileImage(TestConsoleClient.FileExchangeService.GetImageRequestMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileExchangeContract/GetUserProfileImage", ReplyAction="http://tempuri.org/IFileExchangeContract/GetUserProfileImageResponse")]
        System.Threading.Tasks.Task<TestConsoleClient.FileExchangeService.GetImageResponseMessage> GetUserProfileImageAsync(TestConsoleClient.FileExchangeService.GetImageRequestMessage request);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFileExchangeContract/SetUserProfileImage")]
        void SetUserProfileImage(TestConsoleClient.FileExchangeService.AddImageRequestMessage request);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFileExchangeContract/SetUserProfileImage")]
        System.Threading.Tasks.Task SetUserProfileImageAsync(TestConsoleClient.FileExchangeService.AddImageRequestMessage request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetImageRequestMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetImageRequestMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public DataTransferObjects.AuthenticationToken Token;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public int UserId;
        
        public GetImageRequestMessage() {
        }
        
        public GetImageRequestMessage(DataTransferObjects.AuthenticationToken Token, int UserId) {
            this.Token = Token;
            this.UserId = UserId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetImageResponseMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetImageResponseMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public long Length;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream FileByteStream;
        
        public GetImageResponseMessage() {
        }
        
        public GetImageResponseMessage(long Length, System.IO.Stream FileByteStream) {
            this.Length = Length;
            this.FileByteStream = FileByteStream;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AddImageRequestMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class AddImageRequestMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public long Length;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public DataTransferObjects.AuthenticationToken Token;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream FileByteStream;
        
        public AddImageRequestMessage() {
        }
        
        public AddImageRequestMessage(long Length, DataTransferObjects.AuthenticationToken Token, System.IO.Stream FileByteStream) {
            this.Length = Length;
            this.Token = Token;
            this.FileByteStream = FileByteStream;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileExchangeContractChannel : TestConsoleClient.FileExchangeService.IFileExchangeContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileExchangeContractClient : System.ServiceModel.ClientBase<TestConsoleClient.FileExchangeService.IFileExchangeContract>, TestConsoleClient.FileExchangeService.IFileExchangeContract {
        
        public FileExchangeContractClient() {
        }
        
        public FileExchangeContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileExchangeContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileExchangeContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileExchangeContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TestConsoleClient.FileExchangeService.GetImageResponseMessage GetUserProfileImage(TestConsoleClient.FileExchangeService.GetImageRequestMessage request) {
            return base.Channel.GetUserProfileImage(request);
        }
        
        public System.Threading.Tasks.Task<TestConsoleClient.FileExchangeService.GetImageResponseMessage> GetUserProfileImageAsync(TestConsoleClient.FileExchangeService.GetImageRequestMessage request) {
            return base.Channel.GetUserProfileImageAsync(request);
        }
        
        public void SetUserProfileImage(TestConsoleClient.FileExchangeService.AddImageRequestMessage request) {
            base.Channel.SetUserProfileImage(request);
        }
        
        public System.Threading.Tasks.Task SetUserProfileImageAsync(TestConsoleClient.FileExchangeService.AddImageRequestMessage request) {
            return base.Channel.SetUserProfileImageAsync(request);
        }
    }
}