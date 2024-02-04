using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncPriceEngineeringTaskVerificationAcceptedByHead : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskVerificationAcceptedByHeadEvent>
    {
        public SyncPriceEngineeringTaskVerificationAcceptedByHead(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceEngineeringTask model)
        {
            yield return model.UserConstructor;
        }

        protected override IEnumerable<Role> GetRolesForNotification(PriceEngineeringTask model)
        {
            yield return Role.Constructor;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceEngineeringTaskId) => EventServiceClient.PriceEngineeringTaskVerificationAcceptedByHeadPublishEvent(targetUserId, targetRole, priceEngineeringTaskId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskVerificationAcceptedByHead;
    }
}