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
    public class SyncPriceCalculationStart : SyncUnit<PriceCalculation, AfterStartPriceCalculationEvent>
    {
        public SyncPriceCalculationStart(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, PriceCalculation priceCalculation)
        {
            if (user.Roles.Any(userRole => userRole.Role == Role.Pricer)) return true;

            //не нужно показывать уведомление пользователю, который и инициировал этот старт
            if (priceCalculation.Initiator.Id == user.Id) return false;

            if (priceCalculation.FrontManager?.Id == user.Id) return true;

            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.Pricer;
            yield return Role.SalesManager;
        }

        public override bool CurrentUserIsTargetForNotification(PriceCalculation priceCalculation)
        {
            if (GlobalAppProperties.UserIsManager &&
                GlobalAppProperties.User.Id != priceCalculation.FrontManager?.Id) 
                return false;

            return base.CurrentUserIsTargetForNotification(priceCalculation);
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, targetRole, priceCalculationId) => EventServiceClient.StartPriceCalculationPublishEvent(eventSourceAppSessionId, targetUserId, targetRole, priceCalculationId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StartPriceCalculation;
    }
}