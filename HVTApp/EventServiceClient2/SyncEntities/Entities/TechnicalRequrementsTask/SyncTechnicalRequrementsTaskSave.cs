using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncTechnicalRequrementsTaskSave : SyncUnit<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskSave(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (user.Roles.Any(userRole => userRole.Role == Role.BackManagerBoss)) return true;
            if (user.Id == technicalRequrementsTask.FrontManager?.Id) return true;
            if (user.Id == technicalRequrementsTask.BackManager?.Id) return true;
            return false;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId) => EventServiceHost.SaveTechnicalRequarementsTaskPublishEvent(eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SaveTechnicalRequrementsTask;
    }
}