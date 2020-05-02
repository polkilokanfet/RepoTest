using System;
using System.ServiceModel;

namespace EventService
{
    [ServiceContract]
    public interface IEventServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnServiceDisposeEvent();

        [OperationContract(IsOneWay = true)]
        void OnSaveDirectumTaskPublishEvent(Guid taskId);

        [OperationContract(IsOneWay = true)]
        void OnSavePriceCalculationPublishEvent(Guid calculationId);

        [OperationContract(IsOneWay = true)]
        void OnSaveIncomingRequestPublishEvent(Guid requestId);

        [OperationContract(IsOneWay = true)]
        void OnSaveIncomingDocumentPublishEvent(Guid documentId);
    }
}