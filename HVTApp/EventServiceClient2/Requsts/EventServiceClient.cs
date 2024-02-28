using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;

namespace EventServiceClient2
{
    //Тут собраны запросы к хосту
    public partial class EventServiceClient : IEventServiceClient
    {
        public bool PriceEngineeringTaskSendMessagePublishEvent(Guid targetUserId, Role targetRole, Guid messageId)
        {
            return this.EventServiceHost.PriceEngineeringTaskSendMessagePublishEvent(_appSessionId, targetUserId, targetRole, messageId);
        }
    }
}