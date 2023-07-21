using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceEngineeringTasksStart : SyncUnit<PriceEngineeringTasks, PriceEngineeringTasksStartedEvent>
    {
        public SyncPriceEngineeringTasksStart(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, PriceEngineeringTasks priceEngineeringTasks)
        {
            return priceEngineeringTasks.ChildPriceEngineeringTasks.SelectMany(x => x.GetSuitableTasksForInstruct(user)).Any();
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.DesignDepartmentHead;
        }

        public override bool CurrentUserIsTargetForNotification(PriceEngineeringTasks priceEngineeringTasks)
        {
            return GlobalAppProperties.UserIsDesignDepartmentHead;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, priceEngineeringTasksId) => EventServiceHost.PriceEngineeringTasksStartPublishEvent(eventSourceAppSessionId, targetUserId, priceEngineeringTasksId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTasksStart;
    }
}