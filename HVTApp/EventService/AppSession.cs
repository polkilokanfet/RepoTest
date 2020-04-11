using System;
using System.ServiceModel;

namespace EventService
{
    class AppSession
    {
        public Guid AppSessionId { get; }
        public OperationContext OperationContext { get; }

        public AppSession(Guid appSessionId, OperationContext operationContext)
        {
            AppSessionId = appSessionId;
            OperationContext = operationContext;
        }
    }
}