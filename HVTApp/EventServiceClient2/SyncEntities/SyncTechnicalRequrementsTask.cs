using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncTechnicalRequrementsTask : Sync<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTask(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceClient.SaveTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }
}