using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskFinish : SyncUnit<TechnicalRequrementsTask, AfterFinishTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskFinish(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (technicalRequrementsTask.FrontManager?.Id == user.Id) return true;
            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.SalesManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.FinishTechnicalRequarementsTaskPublishEvent(eventSourceAppSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.FinishTechnicalRequrementsTask;

    }
}