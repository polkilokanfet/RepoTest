using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceEngineeringTaskRejectByManager : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskRejectedByManagerEvent>
    {
        public SyncPriceEngineeringTaskRejectByManager(IUnityContainer container) : base(container)
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
                return (eventSourceAppSessionId, targetUserId, priceEngineeringTaskId) => EventServiceHost.PriceEngineeringTaskRejectByManagerPublishEvent(eventSourceAppSessionId, targetUserId, priceEngineeringTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskRejectByManager;
    }
}