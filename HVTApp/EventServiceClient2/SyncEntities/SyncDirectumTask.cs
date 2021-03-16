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
            get { return directumTask => EventServiceHost.SaveDirectumTaskPublishEvent(AppSessionId, directumTask.Id); }
        }

        public SyncDirectumTask(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }
    }
    public class SyncDirectumTaskStart : Sync<DirectumTask, AfterStartDirectumTaskEvent>
    {
        public SyncDirectumTaskStart(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<DirectumTask> PublishEventAction
        {
            get { return directumTask => EventServiceHost.StartDirectumTaskPublishEvent(AppSessionId, directumTask.Id); }
        }
    }
    public class SyncDirectumTaskStop : Sync<DirectumTask, AfterStopDirectumTaskEvent>
    {
        public SyncDirectumTaskStop(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<DirectumTask> PublishEventAction
        {
            get { return directumTask => EventServiceHost.StopDirectumTaskPublishEvent(AppSessionId, directumTask.Id); }
        }
    }

    public class SyncDirectumTaskPerform : Sync<DirectumTask, AfterPerformDirectumTaskEvent>
    {
        public SyncDirectumTaskPerform(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<DirectumTask> PublishEventAction
        {
            get { return directumTask => EventServiceHost.PerformDirectumTaskPublishEvent(AppSessionId, directumTask.Id); }
        }
    }

    public class SyncDirectumTaskAccept: Sync<DirectumTask, AfterAcceptDirectumTaskEvent>
    {
        public SyncDirectumTaskAccept(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<DirectumTask> PublishEventAction
        {
            get { return directumTask => EventServiceHost.AcceptDirectumTaskPublishEvent(AppSessionId, directumTask.Id); }
        }
    }

    public class SyncDirectumTaskReject : Sync<DirectumTask, AfterRejectDirectumTaskEvent>
    {
        public SyncDirectumTaskReject(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<DirectumTask> PublishEventAction
        {
            get { return directumTask => EventServiceHost.RejectDirectumTaskPublishEvent(AppSessionId, directumTask.Id); }
        }
    }

}