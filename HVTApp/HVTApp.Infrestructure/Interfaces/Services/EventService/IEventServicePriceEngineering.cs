using System;
using System.ServiceModel;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public partial interface IEventService
    {
        [OperationContract]
        bool PriceEngineeringTaskStatusChangedEvent(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId);
    }
}