using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskRejectByFrontManager : SyncUnit<TechnicalRequrementsTask, AfterRejectByFrontManagerTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskRejectByFrontManager(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient, IMessageService messageService) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient, messageService)
        {
        }


        protected override IEnumerable<User> GetUsersForNotification(TechnicalRequrementsTask model)
        {
            if (model.BackManager != null)
                yield return model.BackManager;
        }

        protected override IEnumerable<Role> GetRolesForNotification(TechnicalRequrementsTask model)
        {
            yield return Role.BackManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(targetUserId, targetRole, technicalRequarementsTaskId);

        protected override NotificationActionType NotificationActionType => NotificationActionType.RejectByFrontManagerTechnicalRequrementsTask;

    }
}