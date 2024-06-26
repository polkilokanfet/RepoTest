﻿using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncPriceCalculationStart : SyncUnit<PriceCalculation, AfterStartPriceCalculationEvent>
    {
        public SyncPriceCalculationStart(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient, IMessageService messageService) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient, messageService)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(PriceCalculation model)
        {
            return UnitOfWork.Repository<User>().Find(user => user.IsActual && user.Roles.Select(userRole => userRole.Role).Contains(Role.Pricer));
        }

        protected override IEnumerable<Role> GetRolesForNotification(PriceCalculation model)
        {
            yield return Role.Pricer;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, priceCalculationId) => EventServiceClient.StartPriceCalculationPublishEvent(targetUserId, targetRole, priceCalculationId);

        protected override NotificationActionType NotificationActionType => NotificationActionType.StartPriceCalculation;
    }
}