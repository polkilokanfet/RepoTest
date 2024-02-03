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
    public class SyncPriceEngineeringTaskStop : SyncUnit<HVTApp.Model.POCOs.PriceEngineeringTask, PriceEngineeringTaskStoppedEvent>
    {
        public SyncPriceEngineeringTaskStop(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        public override bool IsTargetUser(User user, HVTApp.Model.POCOs.PriceEngineeringTask priceEngineeringTask)
        {
            return priceEngineeringTask.UserConstructor?.Id == user.Id;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.Constructor;
        }

        public override bool CurrentUserIsTargetForNotification(HVTApp.Model.POCOs.PriceEngineeringTask priceEngineeringTask)
        {
            return GlobalAppProperties.UserIsConstructor;
        }


        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, targetRole, priceEngineeringTaskId) => EventServiceClient.PriceEngineeringTaskStopPublishEvent(eventSourceAppSessionId, targetUserId, targetRole, priceEngineeringTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PriceEngineeringTaskStop;
    }
}