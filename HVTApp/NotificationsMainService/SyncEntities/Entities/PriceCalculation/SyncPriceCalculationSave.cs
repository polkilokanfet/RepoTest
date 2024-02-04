using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncPriceCalculationSave : SyncUnit<PriceCalculation, AfterSavePriceCalculationEvent>
    {
        public SyncPriceCalculationSave(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceCalculation model)
        {
            yield break;
        }

        protected override IEnumerable<Role> GetRolesForNotification(PriceCalculation model)
        {
            yield break;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceCalculationId) => EventServiceClient.SavePriceCalculationPublishEvent(targetUserId, targetRole, priceCalculationId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SavePriceCalculation;
    }
}