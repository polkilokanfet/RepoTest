using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncTechnicalRequrementsTask : SyncUnit<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTask(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.SaveTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }

    public class SyncTechnicalRequrementsTaskStart : SyncUnit<TechnicalRequrementsTask, AfterStartTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStart(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.StartTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }

    public class SyncTechnicalRequrementsTaskInstruct : SyncUnit<TechnicalRequrementsTask, AfterInstructTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskInstruct(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.InstructTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }

    public class SyncTechnicalRequrementsTaskReject : SyncUnit<TechnicalRequrementsTask, AfterRejectTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskReject(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.RejectTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }

    public class SyncTechnicalRequrementsTaskRejectByFrontManager : SyncUnit<TechnicalRequrementsTask, AfterRejectByFrontManagerTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskRejectByFrontManager(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }

    public class SyncTechnicalRequrementsTaskFinish : SyncUnit<TechnicalRequrementsTask, AfterFinishTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskFinish(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.FinishTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }

    public class SyncTechnicalRequrementsTaskAccept : SyncUnit<TechnicalRequrementsTask, AfterAcceptTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskAccept(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.AcceptTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }

    public class SyncTechnicalRequrementsTaskStop : SyncUnit<TechnicalRequrementsTask, AfterStopTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStop(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override void DoPublishAction(TechnicalRequrementsTask technicalRequrementsTask)
        {
            this.EventServiceHost.StopTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id);
        }
    }
}