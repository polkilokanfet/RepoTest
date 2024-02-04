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
    public class SyncPriceCalculationReject : SyncUnit<PriceCalculation, AfterRejectPriceCalculationEvent>
    {
        public SyncPriceCalculationReject(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceCalculation model)
        {
            return model.PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Project.Manager).Distinct();
        }

        protected override IEnumerable<Role> GetRolesForNotification(PriceCalculation model)
        {
            yield return Role.SalesManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceCalculationId) => EventServiceClient.RejectPriceCalculationPublishEvent(targetUserId, targetRole, priceCalculationId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.RejectPriceCalculation;
    }
}