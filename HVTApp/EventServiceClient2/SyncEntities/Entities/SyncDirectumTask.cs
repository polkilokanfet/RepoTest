using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncDirectumTask : SyncUnit<DirectumTask, AfterSaveDirectumTaskEvent>
    {

        public SyncDirectumTask(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(DirectumTask directumTask)
        {
            EventServiceHost.SaveDirectumTaskPublishEvent(AppSessionId, directumTask.Id);
        }
    }

    public class SyncDirectumTaskStart : SyncUnit<DirectumTask, AfterStartDirectumTaskEvent>
    {
        public SyncDirectumTaskStart(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(DirectumTask directumTask)
        {
            EventServiceHost.StartDirectumTaskPublishEvent(AppSessionId, directumTask.Id);
        }
    }

    public class SyncDirectumTaskStop : SyncUnit<DirectumTask, AfterStopDirectumTaskEvent>
    {
        public SyncDirectumTaskStop(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(DirectumTask directumTask)
        {
            EventServiceHost.StopDirectumTaskPublishEvent(AppSessionId, directumTask.Id);
        }
    }

    public class SyncDirectumTaskPerform : SyncUnit<DirectumTask, AfterPerformDirectumTaskEvent>
    {
        public SyncDirectumTaskPerform(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(DirectumTask directumTask)
        {
            EventServiceHost.PerformDirectumTaskPublishEvent(AppSessionId, directumTask.Id);
        }
    }

    public class SyncDirectumTaskAccept: SyncUnit<DirectumTask, AfterAcceptDirectumTaskEvent>
    {
        public SyncDirectumTaskAccept(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(DirectumTask directumTask)
        {
            EventServiceHost.AcceptDirectumTaskPublishEvent(AppSessionId, directumTask.Id);
        }
    }

    public class SyncDirectumTaskReject : SyncUnit<DirectumTask, AfterRejectDirectumTaskEvent>
    {
        public SyncDirectumTaskReject(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(DirectumTask directumTask)
        {
            EventServiceHost.RejectDirectumTaskPublishEvent(AppSessionId, directumTask.Id);
        }
    }

}