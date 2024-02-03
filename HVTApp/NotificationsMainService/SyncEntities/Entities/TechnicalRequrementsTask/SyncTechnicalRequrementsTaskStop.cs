using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskStop : SyncUnit<TechnicalRequrementsTask, AfterStopTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStop(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (technicalRequrementsTask.BackManager?.Id == user.Id) return true;
            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.BackManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.StopTechnicalRequarementsTaskPublishEvent(eventSourceAppSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StopTechnicalRequrementsTask;

    }
}