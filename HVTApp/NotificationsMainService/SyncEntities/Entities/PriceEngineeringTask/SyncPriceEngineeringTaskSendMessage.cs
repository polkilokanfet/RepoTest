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
    public class SyncPriceEngineeringTaskSendMessage : SyncUnit<PriceEngineeringTaskMessage, PriceEngineeringTaskSendMessageEvent>
    {
        public SyncPriceEngineeringTaskSendMessage(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, PriceEngineeringTaskMessage message)
        {
            if (message.Author?.Id == user.Id) return false;

            var priceEngineeringTask = UnitOfWork.Repository<HVTApp.Model.POCOs.PriceEngineeringTask>().GetById(message.PriceEngineeringTaskId);
            if (priceEngineeringTask != null)
            {
                if (user.Id == priceEngineeringTask.UserConstructor?.Id) return true;

                if (user.Id == priceEngineeringTask.GetPriceEngineeringTasks(UnitOfWork)?.UserManager?.Id) return true;
            }

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
                return (eventSourceAppSessionId, targetUserId, targetRole, messageId) => EventServiceClient.PriceEngineeringTaskSendMessagePublishEvent(eventSourceAppSessionId, targetUserId, targetRole, messageId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskSendMessage;
    }
}