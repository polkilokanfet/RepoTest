using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncIncomingRequest : SyncUnit<IncomingRequest, AfterSaveIncomingRequestEvent>
    {
        public SyncIncomingRequest(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, IncomingRequest model)
        {
            //TODO: implement
            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, targetRole, requestId) => EventServiceClient.SaveIncomingRequestPublishEvent(eventSourceAppSessionId, targetUserId, targetRole, requestId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SaveIncomingRequest;
    }
}