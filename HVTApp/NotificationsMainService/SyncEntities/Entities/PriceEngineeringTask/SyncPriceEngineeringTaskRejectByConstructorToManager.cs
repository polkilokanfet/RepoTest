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
    public class SyncPriceEngineeringTaskRejectByConstructorToManager : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskRejectedByConstructorEvent>
    {
        public SyncPriceEngineeringTaskRejectByConstructorToManager(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceEngineeringTask model)
        {
            yield return model.GetPriceEngineeringTasks(UnitOfWork).UserManager;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.SalesManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceEngineeringTaskId) => EventServiceClient.PriceEngineeringTaskRejectByConstructorPublishEvent(targetUserId, targetRole, priceEngineeringTaskId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskRejectByConstructor;
    }
}