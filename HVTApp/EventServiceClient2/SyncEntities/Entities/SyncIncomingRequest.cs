using System;
using System.Collections.Generic;
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

        public override bool IsTargetUser(User user, IncomingRequest model)
        {
            throw new NotImplementedException();
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, requestId) => EventServiceHost.SaveIncomingRequestPublishEvent(eventSourceAppSessionId, targetUserId, requestId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SaveIncomingRequest;
    }
}