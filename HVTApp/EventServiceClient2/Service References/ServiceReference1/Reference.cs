﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventServiceClient2.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IEventService", CallbackContract=typeof(EventServiceClient2.ServiceReference1.IEventServiceCallback))]
    public interface IEventService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/Connect", ReplyAction="http://tempuri.org/IEventService/ConnectResponse")]
        bool Connect(System.Guid appSessionId, System.Guid userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/Connect", ReplyAction="http://tempuri.org/IEventService/ConnectResponse")]
        System.Threading.Tasks.Task<bool> ConnectAsync(System.Guid appSessionId, System.Guid userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/Disconnect", ReplyAction="http://tempuri.org/IEventService/DisconnectResponse")]
        void Disconnect(System.Guid appSessionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/Disconnect", ReplyAction="http://tempuri.org/IEventService/DisconnectResponse")]
        System.Threading.Tasks.Task DisconnectAsync(System.Guid appSessionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/SendMessageToChat", ReplyAction="http://tempuri.org/IEventService/SendMessageToChatResponse")]
        void SendMessageToChat(System.Guid authorId, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/SendMessageToChat", ReplyAction="http://tempuri.org/IEventService/SendMessageToChatResponse")]
        System.Threading.Tasks.Task SendMessageToChatAsync(System.Guid authorId, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/SendMessageToUser", ReplyAction="http://tempuri.org/IEventService/SendMessageToUserResponse")]
        void SendMessageToUser(System.Guid authorId, System.Guid recipientId, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEventService/SendMessageToUser", ReplyAction="http://tempuri.org/IEventService/SendMessageToUserResponse")]
        System.Threading.Tasks.Task SendMessageToUserAsync(System.Guid authorId, System.Guid recipientId, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SaveDirectumTaskPublishEvent")]
        void SaveDirectumTaskPublishEvent(System.Guid appSessionId, System.Guid taskId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SaveDirectumTaskPublishEvent")]
        System.Threading.Tasks.Task SaveDirectumTaskPublishEventAsync(System.Guid appSessionId, System.Guid taskId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SavePriceCalculationPublishEvent")]
        void SavePriceCalculationPublishEvent(System.Guid appSessionId, System.Guid priceCalculationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SavePriceCalculationPublishEvent")]
        System.Threading.Tasks.Task SavePriceCalculationPublishEventAsync(System.Guid appSessionId, System.Guid priceCalculationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SaveIncomingRequestPublishEvent")]
        void SaveIncomingRequestPublishEvent(System.Guid appSessionId, System.Guid requestId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SaveIncomingRequestPublishEvent")]
        System.Threading.Tasks.Task SaveIncomingRequestPublishEventAsync(System.Guid appSessionId, System.Guid requestId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SaveIncomingDocumentPublishEvent")]
        void SaveIncomingDocumentPublishEvent(System.Guid appSessionId, System.Guid documentId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/SaveIncomingDocumentPublishEvent")]
        System.Threading.Tasks.Task SaveIncomingDocumentPublishEventAsync(System.Guid appSessionId, System.Guid documentId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEventServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/OnServiceDisposeEvent")]
        void OnServiceDisposeEvent();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/OnSendMessageToChat")]
        void OnSendMessageToChat(System.Guid authorId, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/OnSendMessageToUser")]
        void OnSendMessageToUser(System.Guid authorId, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/OnSaveDirectumTaskPublishEvent")]
        void OnSaveDirectumTaskPublishEvent(System.Guid taskId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/OnSavePriceCalculationPublishEvent")]
        void OnSavePriceCalculationPublishEvent(System.Guid calculationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/OnSaveIncomingRequestPublishEvent")]
        void OnSaveIncomingRequestPublishEvent(System.Guid requestId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IEventService/OnSaveIncomingDocumentPublishEvent")]
        void OnSaveIncomingDocumentPublishEvent(System.Guid documentId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEventServiceChannel : EventServiceClient2.ServiceReference1.IEventService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EventServiceClient : System.ServiceModel.DuplexClientBase<EventServiceClient2.ServiceReference1.IEventService>, EventServiceClient2.ServiceReference1.IEventService {
        
        public EventServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public EventServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public EventServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public EventServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public EventServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool Connect(System.Guid appSessionId, System.Guid userId) {
            return base.Channel.Connect(appSessionId, userId);
        }
        
        public System.Threading.Tasks.Task<bool> ConnectAsync(System.Guid appSessionId, System.Guid userId) {
            return base.Channel.ConnectAsync(appSessionId, userId);
        }
        
        public void Disconnect(System.Guid appSessionId) {
            base.Channel.Disconnect(appSessionId);
        }
        
        public System.Threading.Tasks.Task DisconnectAsync(System.Guid appSessionId) {
            return base.Channel.DisconnectAsync(appSessionId);
        }
        
        public void SendMessageToChat(System.Guid authorId, string message) {
            base.Channel.SendMessageToChat(authorId, message);
        }
        
        public System.Threading.Tasks.Task SendMessageToChatAsync(System.Guid authorId, string message) {
            return base.Channel.SendMessageToChatAsync(authorId, message);
        }
        
        public void SendMessageToUser(System.Guid authorId, System.Guid recipientId, string message) {
            base.Channel.SendMessageToUser(authorId, recipientId, message);
        }
        
        public System.Threading.Tasks.Task SendMessageToUserAsync(System.Guid authorId, System.Guid recipientId, string message) {
            return base.Channel.SendMessageToUserAsync(authorId, recipientId, message);
        }
        
        public void SaveDirectumTaskPublishEvent(System.Guid appSessionId, System.Guid taskId) {
            base.Channel.SaveDirectumTaskPublishEvent(appSessionId, taskId);
        }
        
        public System.Threading.Tasks.Task SaveDirectumTaskPublishEventAsync(System.Guid appSessionId, System.Guid taskId) {
            return base.Channel.SaveDirectumTaskPublishEventAsync(appSessionId, taskId);
        }
        
        public void SavePriceCalculationPublishEvent(System.Guid appSessionId, System.Guid priceCalculationId) {
            base.Channel.SavePriceCalculationPublishEvent(appSessionId, priceCalculationId);
        }
        
        public System.Threading.Tasks.Task SavePriceCalculationPublishEventAsync(System.Guid appSessionId, System.Guid priceCalculationId) {
            return base.Channel.SavePriceCalculationPublishEventAsync(appSessionId, priceCalculationId);
        }
        
        public void SaveIncomingRequestPublishEvent(System.Guid appSessionId, System.Guid requestId) {
            base.Channel.SaveIncomingRequestPublishEvent(appSessionId, requestId);
        }
        
        public System.Threading.Tasks.Task SaveIncomingRequestPublishEventAsync(System.Guid appSessionId, System.Guid requestId) {
            return base.Channel.SaveIncomingRequestPublishEventAsync(appSessionId, requestId);
        }
        
        public void SaveIncomingDocumentPublishEvent(System.Guid appSessionId, System.Guid documentId) {
            base.Channel.SaveIncomingDocumentPublishEvent(appSessionId, documentId);
        }
        
        public System.Threading.Tasks.Task SaveIncomingDocumentPublishEventAsync(System.Guid appSessionId, System.Guid documentId) {
            return base.Channel.SaveIncomingDocumentPublishEventAsync(appSessionId, documentId);
        }
    }
}