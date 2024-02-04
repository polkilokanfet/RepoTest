using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskReject : SyncUnit<TechnicalRequrementsTask, AfterRejectTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskReject(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(TechnicalRequrementsTask model)
        {
            if (model.FrontManager != null)
                yield return model.FrontManager;
        }

        protected override IEnumerable<Role> GetRolesForNotification(TechnicalRequrementsTask model)
        {
            yield return Role.SalesManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.RejectTechnicalRequarementsTaskPublishEvent(targetUserId, targetRole, technicalRequarementsTaskId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.RejectTechnicalRequrementsTask;

    }
}