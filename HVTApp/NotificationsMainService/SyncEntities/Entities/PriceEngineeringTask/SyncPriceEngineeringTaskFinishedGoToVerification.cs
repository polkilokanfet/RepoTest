using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model;
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

        public override bool IsTargetUser(User user, PriceEngineeringTask priceEngineeringTask)
        {
            return priceEngineeringTask.DesignDepartment.Head.Id == user.Id;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.DesignDepartmentHead;
        }

        public override bool CurrentUserIsTargetForNotification(PriceEngineeringTask priceEngineeringTask)
        {
            return GlobalAppProperties.UserIsDesignDepartmentHead;
        }


        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (targetUserId, targetRole, priceEngineeringTaskId) => EventServiceClient.PriceEngineeringTaskFinishGoToVerificationPublishEvent(targetUserId, targetRole, priceEngineeringTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskFinishGoToVerification;
    }
}