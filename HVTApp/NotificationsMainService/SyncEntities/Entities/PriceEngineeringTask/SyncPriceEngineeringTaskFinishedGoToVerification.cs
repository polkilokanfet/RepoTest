using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncPriceEngineeringTaskFinishedGoToVerification : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskFinishedGoToVerificationEvent>
    {
        public SyncPriceEngineeringTaskFinishedGoToVerification(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceEngineeringTask model)
        {
            yield return model.DesignDepartment.Head;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.DesignDepartmentHead;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceEngineeringTaskId) => EventServiceClient.PriceEngineeringTaskFinishGoToVerificationPublishEvent(targetUserId, targetRole, priceEngineeringTaskId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskFinishGoToVerification;
    }
}