using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskSave : SyncUnit<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskSave(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (user.Roles.Any(userRole => userRole.Role == Role.BackManagerBoss)) return true;
            if (user.Id == technicalRequrementsTask.FrontManager?.Id) return true;
            if (user.Id == technicalRequrementsTask.BackManager?.Id) return true;
            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.SaveTechnicalRequarementsTaskPublishEvent(targetUserId, targetRole, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SaveTechnicalRequrementsTask;
    }
}