using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncTechnicalRequrementsTaskInstruct : SyncUnit<TechnicalRequrementsTask, AfterInstructTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskInstruct(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (technicalRequrementsTask.BackManager?.Id == user.Id) return true;
            if (technicalRequrementsTask.FrontManager?.Id == user.Id) return true;
            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId) => EventServiceHost.InstructTechnicalRequarementsTaskPublishEvent(eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.InstructTechnicalRequrementsTask;

    }
}