using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncPriceEngineeringTaskStart : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskStartedEvent>
    {
        public SyncPriceEngineeringTaskStart(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceEngineeringTask model)
        {
            if (model.UserConstructor != null)
                yield return model.UserConstructor;
            else if (model.DesignDepartment != null)
                yield return model.DesignDepartment.Head;
        }

        protected override IEnumerable<Role> GetRolesForNotification(PriceEngineeringTask model)
        {
            if (model.UserConstructor != null)
                yield return Role.Constructor;
            else if (model.DesignDepartment != null)
                yield return Role.DesignDepartmentHead;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceEngineeringTaskId) => EventServiceClient.PriceEngineeringTaskStartPublishEvent(targetUserId, targetRole, priceEngineeringTaskId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskStart;
    }
}