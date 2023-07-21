using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculationCancel : SyncUnit<PriceCalculation, AfterStopPriceCalculationEvent>
    {
        public SyncPriceCalculationCancel(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, PriceCalculation priceCalculation)
        {
            if (user.Roles.Any(userRole => userRole.Role == Role.Pricer)) return true;
            if (priceCalculation.FrontManager?.Id == user.Id)
                if (priceCalculation.Initiator.Id != priceCalculation.FrontManager?.Id)
                    return true;
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
                return (eventSourceAppSessionId, targetUserId, priceCalculationId) => EventServiceHost.CancelPriceCalculationPublishEvent(eventSourceAppSessionId, targetUserId, priceCalculationId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.CancelPriceCalculation;

    }
}
