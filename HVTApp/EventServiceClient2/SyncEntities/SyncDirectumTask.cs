using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncDirectumTask : Sync<DirectumTask, AfterSaveDirectumTaskEvent>
    {

        protected override Action<DirectumTask> PublishEventAction
        {
            get { return directumTask => EventServiceClient.SaveDirectumTaskPublishEvent(AppSessionId, directumTask.Id); }
        }

        public SyncDirectumTask(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }
    }
}