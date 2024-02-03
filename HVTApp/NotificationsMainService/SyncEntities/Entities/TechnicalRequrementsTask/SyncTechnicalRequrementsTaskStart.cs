using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskStart : SyncUnit<TechnicalRequrementsTask, AfterStartTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStart(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (user.Roles.Any(userRole => userRole.Role == Role.BackManagerBoss)) return true;
            if (technicalRequrementsTask.BackManager?.Id == user.Id) return true;
            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.BackManager;
            yield return Role.BackManagerBoss;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.StartTechnicalRequarementsTaskPublishEvent(targetUserId, targetRole, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StartTechnicalRequrementsTask;

    }
}