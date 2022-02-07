using System.Linq;
using HVTApp.Infrastructure;
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
            if (priceCalculation.Initiator.Id == user.Id) return true;
            if (user.Roles.Any(userRole => userRole.Role == Role.Pricer)) return true;
            if (priceCalculation.FrontManager?.Id == user.Id) return true;
            return false;
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
