using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncTechnicalRequrementsTask : Sync<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTask(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceHost.SaveTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }

    public class SyncTechnicalRequrementsTaskStart : Sync<TechnicalRequrementsTask, AfterStartTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStart(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceHost.StartTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }

    public class SyncTechnicalRequrementsTaskInstruct : Sync<TechnicalRequrementsTask, AfterInstructTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskInstruct(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceHost.InstructTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }
    public class SyncTechnicalRequrementsTaskReject : Sync<TechnicalRequrementsTask, AfterRejectTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskReject(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceHost.RejectTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }
    public class SyncTechnicalRequrementsTaskCancel : Sync<TechnicalRequrementsTask, AfterCancelTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskCancel(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceHost.CancelTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }
    public class SyncTechnicalRequrementsTaskFinish : Sync<TechnicalRequrementsTask, AfterFinishTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskFinish(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceHost.FinishTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }
    public class SyncTechnicalRequrementsTaskAccept : Sync<TechnicalRequrementsTask, AfterAcceptTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskAccept(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceHost.AcceptTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }

}