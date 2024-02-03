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
    public class SyncPriceCalculationReject : SyncUnit<PriceCalculation, AfterRejectPriceCalculationEvent>
    {
        public SyncPriceCalculationReject(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, PriceCalculation priceCalculation)
        {
            if (priceCalculation.Initiator.Id == user.Id) return true;
            //if (user.Roles.Any(userRole => userRole.Role == Role.Pricer)) return true;
            if (priceCalculation.FrontManager?.Id == user.Id) return true;
            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.SalesManager;
            yield return Role.BackManager;
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
                return (targetUserId, targetRole, priceCalculationId) => EventServiceClient.RejectPriceCalculationPublishEvent(targetUserId, targetRole, priceCalculationId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.RejectPriceCalculation;
    }
}