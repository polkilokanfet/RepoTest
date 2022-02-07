using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncDirectumTaskPerform : SyncUnit<DirectumTask, AfterPerformDirectumTaskEvent>
    {
        public SyncDirectumTaskPerform(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, DirectumTask directumTask)
        {
            if (directumTask.Group.Author.Id == user.Id) return true;
            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, taskId) => EventServiceHost.PerformDirectumTaskPublishEvent(eventSourceAppSessionId, targetUserId, taskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.PerformDirectumTask;

    }
}