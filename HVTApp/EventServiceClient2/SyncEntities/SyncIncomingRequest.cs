using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncIncomingRequest : Sync<IncomingRequest, AfterSaveIncomingRequestEvent>
    {
        public SyncIncomingRequest(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<IncomingRequest> PublishEventAction
        {
            get { return incomingRequest => this.EventServiceClient.SaveIncomingRequestPublishEvent(AppSessionId, incomingRequest.Id); }
        }
    }
}