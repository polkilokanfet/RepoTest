using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceEngineeringTaskSendMessage : SyncUnit<PriceEngineeringTaskMessage, PriceEngineeringTaskSendMessageEvent>
    {
        public SyncPriceEngineeringTaskSendMessage(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, PriceEngineeringTaskMessage message)
        {
            if (message.Author?.Id == user.Id) return false;

            var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(message.PriceEngineeringTaskId);
            if (user.Id == priceEngineeringTask.UserConstructor.Id) return true;

            if (priceEngineeringTask.GetPriceEngineeringTasks(UnitOfWork)?.UserManager?.Id == user.Id) return true;

            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.SalesManager;
            yield return Role.DesignDepartmentHead;
            yield return Role.Constructor;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, messageId) => EventServiceHost.PriceEngineeringTaskSendMessagePublishEvent(eventSourceAppSessionId, targetUserId, messageId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskSendMessage;
    }
}