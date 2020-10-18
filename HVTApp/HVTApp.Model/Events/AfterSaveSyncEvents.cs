using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{
    public class AfterSaveDirectumTaskSyncEvent : PubSubEvent<DirectumTask> { }
    public class AfterSavePriceCalculationSyncEvent : PubSubEvent<PriceCalculation> { }
    public class AfterSaveIncomingRequestSyncEvent : PubSubEvent<IncomingRequest> { }
    public class AfterSaveIncomingDocumentSyncEvent : PubSubEvent<Document> { }
    public class AfterFinishPriceCalculationSyncEvent : PubSubEvent<PriceCalculation> { }
}