using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncDirectumTaskReject : SyncUnit<DirectumTask, AfterRejectDirectumTaskEvent>
    {
        public SyncDirectumTaskReject(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, DirectumTask directumTask)
        {
            if (directumTask.StartResult.HasValue && directumTask.Performer?.Id == user.Id)
                return true;

            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, taskId) => EventServiceHost.RejectDirectumTaskPublishEvent(eventSourceAppSessionId, targetUserId, taskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.RejectDirectumTask;
    }
}