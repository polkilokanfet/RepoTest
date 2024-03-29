﻿using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace NotificationsMainService.SyncEntities.Entities
{
    public class SyncTechnicalRequrementsTaskStop : SyncUnit<TechnicalRequrementsTask, AfterStopTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStop(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
        {
        }


        protected override IEnumerable<User> GetUsersForNotification(TechnicalRequrementsTask model)
        {
            if (model.BackManager != null)
                yield return model.BackManager;
        }

        protected override IEnumerable<Role> GetRolesForNotification(TechnicalRequrementsTask model)
        {
            yield return Role.BackManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser => 
            (targetUserId, targetRole, technicalRequarementsTaskId) => EventServiceClient.StopTechnicalRequarementsTaskPublishEvent(targetUserId, targetRole, technicalRequarementsTaskId);

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StopTechnicalRequrementsTask;

    }
}