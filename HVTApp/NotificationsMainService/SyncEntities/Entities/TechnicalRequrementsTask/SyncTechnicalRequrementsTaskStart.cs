﻿using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskStart : SyncUnit<TechnicalRequrementsTask, AfterStartTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStart(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }

        protected override IEnumerable<User> GetUsersForNotification(TechnicalRequrementsTask model)
        {
            if (model.BackManager != null)
                yield return model.BackManager;
            else
                foreach (var user in UnitOfWork.Repository<User>().Find(x => x.Roles.Select(xx => xx.Role).Contains(Role.BackManagerBoss)))
                    yield return user;
        }

        protected override IEnumerable<Role> GetRolesForNotification(TechnicalRequrementsTask model)
        {
            if (model.BackManager != null)
                yield return Role.BackManager;
            else
                yield return Role.BackManagerBoss;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.StartTechnicalRequarementsTaskPublishEvent(targetUserId, targetRole, technicalRequarementsTaskId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StartTechnicalRequrementsTask;

    }
}