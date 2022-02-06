using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncIncomingRequest : SyncUnit<IncomingRequest, AfterSaveIncomingRequestEvent>
    {
        public SyncIncomingRequest(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(IncomingRequest incomingRequest)
        {
            this.EventServiceHost.SaveIncomingRequestPublishEvent(AppSessionId, incomingRequest.Id);
        }
    }
}