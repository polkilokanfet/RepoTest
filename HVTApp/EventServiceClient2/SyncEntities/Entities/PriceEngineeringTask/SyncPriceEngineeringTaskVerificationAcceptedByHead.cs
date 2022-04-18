using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceEngineeringTaskVerificationAcceptedByHead : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskVerificationAcceptedByHeadEvent>
    {
        public SyncPriceEngineeringTaskVerificationAcceptedByHead(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, PriceEngineeringTask priceEngineeringTask)
        {
            return priceEngineeringTask.UserConstructor?.Id == user.Id;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.Constructor;
        }

        public override bool CurrentUserIsTargetForNotification(PriceEngineeringTask priceEngineeringTask)
        {
            return GlobalAppProperties.User.RoleCurrent == Role.Constructor;
        }


        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, priceEngineeringTaskId) => EventServiceHost.PriceEngineeringTaskVerificationAcceptedByHeadPublishEvent(eventSourceAppSessionId, targetUserId, priceEngineeringTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskVerificationAcceptedByHead;
    }
    public class SyncPriceEngineeringTaskVerificationRejectedByHead : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskVerificationRejectedByHeadEvent>
    {
        public SyncPriceEngineeringTaskVerificationRejectedByHead(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, PriceEngineeringTask priceEngineeringTask)
        {
            return priceEngineeringTask.UserConstructor?.Id == user.Id;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.Constructor;
        }

        public override bool CurrentUserIsTargetForNotification(PriceEngineeringTask priceEngineeringTask)
        {
            return GlobalAppProperties.User.RoleCurrent == Role.Constructor;
        }


        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, priceEngineeringTaskId) => EventServiceHost.PriceEngineeringTaskVerificationRejectedByHeadPublishEvent(eventSourceAppSessionId, targetUserId, priceEngineeringTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskVerificationRejectedByHead;
    }

}