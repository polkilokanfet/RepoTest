using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{

    public class AfterFinishPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

    public class AfterStopPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

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

    //#region ActualPayment

    public class AfterSaveActualPaymentDocumentEvent : PubSubEvent<PaymentDocument> { }

    //public class AfterSaveActualPaymentDocumentEvent : PubSubEvent<ActualPaymentEventEntity> { }
    //public class AfterSaveActualPaymentEvent : PubSubEvent<SalesUnit> { }

    //#endregion
}