using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceEngineeringTaskRejectByConstructor : SyncUnit<PriceEngineeringTask, PriceEngineeringTaskRejectedByConstructorEvent>
    {
        public SyncPriceEngineeringTaskRejectByConstructor(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, PriceEngineeringTask priceEngineeringTask)
        {
            PriceEngineeringTask task = priceEngineeringTask;
            while (task.ParentPriceEngineeringTasksId.HasValue == false)
            {
                if (task.ParentPriceEngineeringTaskId.HasValue)
                {
                    task = UnitOfWork.Repository<PriceEngineeringTask>().GetById(task.ParentPriceEngineeringTaskId.Value);
                }
                else
                {
                    return false;
                }
            }

            PriceEngineeringTasks priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(task.ParentPriceEngineeringTasksId.Value);

            return priceEngineeringTasks.UserManager?.Id == user.Id;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.SalesManager;
        }

        public override bool CurrentUserIsTargetForNotification(PriceEngineeringTask priceEngineeringTask)
        {
            return GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        }


        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, priceEngineeringTaskId) => EventServiceHost.PriceEngineeringTaskRejectByConstructorPublishEvent(eventSourceAppSessionId, targetUserId, priceEngineeringTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskRejectByConstructor;
    }
}