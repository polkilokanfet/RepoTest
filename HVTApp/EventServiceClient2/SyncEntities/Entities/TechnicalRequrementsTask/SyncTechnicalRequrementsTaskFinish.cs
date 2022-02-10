using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncTechnicalRequrementsTaskFinish : SyncUnit<TechnicalRequrementsTask, AfterFinishTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskFinish(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (technicalRequrementsTask.FrontManager?.Id == user.Id) return true;
            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.SalesManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId) => EventServiceHost.FinishTechnicalRequarementsTaskPublishEvent(eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.FinishTechnicalRequrementsTask;

    }
}