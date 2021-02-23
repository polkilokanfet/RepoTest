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

    public class SyncTechnicalRequrementsTaskStart : Sync<TechnicalRequrementsTask, AfterStartTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskStart(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceClient.StartTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }

    public class SyncTechnicalRequrementsTaskInstruct : Sync<TechnicalRequrementsTask, AfterInstructTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskInstruct(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceClient.InstructTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }
    public class SyncTechnicalRequrementsTaskReject : Sync<TechnicalRequrementsTask, AfterRejectTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskReject(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceClient.RejectTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }
    public class SyncTechnicalRequrementsTaskCancel : Sync<TechnicalRequrementsTask, AfterCancelTechnicalRequrementsTaskEvent>
    {
        public SyncTechnicalRequrementsTaskCancel(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<TechnicalRequrementsTask> PublishEventAction
        {
            get { return technicalRequrementsTask => this.EventServiceClient.CancelTechnicalRequarementsTaskPublishEvent(AppSessionId, technicalRequrementsTask.Id); }
        }
    }

}