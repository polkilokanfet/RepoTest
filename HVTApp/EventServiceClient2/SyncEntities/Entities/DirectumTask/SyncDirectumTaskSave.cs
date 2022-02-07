using System;
using System.Collections.Generic;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncDirectumTaskSave : SyncUnit<DirectumTask, AfterSaveDirectumTaskEvent>
    {
        public SyncDirectumTaskSave(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, DirectumTask directumTask)
        {
            if (directumTask.Performer.Id == user.Id) return true;
            if (directumTask.Group.Author.Id == user.Id) return true;
            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, taskId) => EventServiceHost.SaveDirectumTaskPublishEvent(eventSourceAppSessionId, targetUserId, taskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SaveDirectumTask;
    }
}