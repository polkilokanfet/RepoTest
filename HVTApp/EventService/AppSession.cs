using System;
using System.ServiceModel;

namespace EventService
{
    class AppSession
    {
        public Guid AppSessionId { get; }
        public Guid UserId { get; }
        public OperationContext OperationContext { get; }

        public AppSession(Guid appSessionId, Guid userId, OperationContext operationContext)
        {
            AppSessionId = appSessionId;
            UserId = userId;
            OperationContext = operationContext;
        }

        public override string ToString()
        {
            return $"appSessionId: {AppSessionId}, userId: {UserId}";
        }
    }
}