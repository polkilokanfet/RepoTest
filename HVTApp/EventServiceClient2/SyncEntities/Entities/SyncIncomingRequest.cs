using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncIncomingRequest : SyncUnit<IncomingRequest, AfterSaveIncomingRequestEvent>
    {
        public SyncIncomingRequest(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<IncomingRequest> PublishEventAction
        {
            get { return incomingRequest => this.EventServiceHost.SaveIncomingRequestPublishEvent(AppSessionId, incomingRequest.Id); }
        }
    }
}