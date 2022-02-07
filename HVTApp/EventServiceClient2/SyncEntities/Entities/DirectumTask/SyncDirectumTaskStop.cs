using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncDirectumTaskStop : SyncUnit<DirectumTask, AfterStopDirectumTaskEvent>
    {
        public SyncDirectumTaskStop(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, DirectumTask directumTask)
        {
            if (directumTask.Performer?.Id == user.Id) return true;

            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, taskId) => EventServiceHost.StopDirectumTaskPublishEvent(eventSourceAppSessionId, targetUserId, taskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.StopDirectumTask;

    }
}