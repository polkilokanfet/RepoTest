using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncDirectumTaskPerform : SyncUnit<DirectumTask, AfterPerformDirectumTaskEvent>
    {
        public SyncDirectumTaskPerform(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, DirectumTask directumTask)
        {
            if (directumTask.Group.Author.Id == user.Id) return true;
            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, targetRole, taskId) => EventServiceClient.PerformDirectumTaskPublishEvent(eventSourceAppSessionId, targetUserId, targetRole, taskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PerformDirectumTask;

    }
}