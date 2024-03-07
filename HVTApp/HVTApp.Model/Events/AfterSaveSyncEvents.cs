using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{

    #region Directum

    public class AfterStartDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterStopDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterPerformDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterAcceptDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterRejectDirectumTaskEvent : PubSubEvent<DirectumTask> { }

    #endregion

    #region PriceEngineeringTasks

    public class PriceEngineeringTaskSendMessageEvent : PubSubEvent<PriceEngineeringTaskMessage> { }
    public class PriceEngineeringTaskReciveMessageEvent : PubSubEvent<PriceEngineeringTaskMessage> { }

    #endregion

    public class AfterSaveActualPaymentDocumentEvent : PubSubEvent<PaymentDocument> { }
}