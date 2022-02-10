using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncTechnicalRequrementsTaskAccept : SyncUnit<TechnicalRequrementsTask, AfterAcceptTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskAccept(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, TechnicalRequrementsTask technicalRequrementsTask)
        {
            if (technicalRequrementsTask.BackManager?.Id == user.Id) return true;
            return false;
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.BackManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId) => EventServiceHost.AcceptTechnicalRequarementsTaskPublishEvent(eventSourceAppSessionId, targetUserId, technicalRequarementsTaskId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.AcceptTechnicalRequrementsTask;

    }
}