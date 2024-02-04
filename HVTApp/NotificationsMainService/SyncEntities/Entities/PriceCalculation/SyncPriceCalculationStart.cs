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
    public class SyncPriceCalculationStart : SyncUnit<PriceCalculation, AfterStartPriceCalculationEvent>
    {
        public SyncPriceCalculationStart(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceCalculation model)
        {
            return UnitOfWork.Repository<User>().Find(user => user.IsActual && user.Roles.Select(userRole => userRole.Role).Contains(Role.Pricer));
        }

        protected override IEnumerable<Role> GetRolesForNotification(PriceCalculation model)
        {
            yield return Role.Pricer;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceCalculationId) => EventServiceClient.StartPriceCalculationPublishEvent(targetUserId, targetRole, priceCalculationId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StartPriceCalculation;
    }
}