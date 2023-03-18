using System;
using System.ServiceModel;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public partial interface IEventServiceCallback
    {
        [OperationContract]
        bool OnPriceEngineeringEventCallback(Guid userAuthorId, Guid priceEngineeringTaskId);
    }
}