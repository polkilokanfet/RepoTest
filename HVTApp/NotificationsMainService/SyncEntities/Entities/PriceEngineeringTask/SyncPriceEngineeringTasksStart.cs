using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncPriceEngineeringTasksStart : SyncUnit<PriceEngineeringTasks, PriceEngineeringTasksStartedEvent>
    {
        public SyncPriceEngineeringTasksStart(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, PriceEngineeringTasks priceEngineeringTasks)
        {
            return priceEngineeringTasks.ChildPriceEngineeringTasks.SelectMany(x => x.GetSuitableTasksForInstruct(user)).Any();
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.DesignDepartmentHead;
        }

        public override bool CurrentUserIsTargetForNotification(PriceEngineeringTasks priceEngineeringTasks)
        {
            return GlobalAppProperties.UserIsDesignDepartmentHead;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (targetUserId, targetRole, priceEngineeringTasksId) => EventServiceClient.PriceEngineeringTasksStartPublishEvent(targetUserId, targetRole, priceEngineeringTasksId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTasksStart;
    }
}