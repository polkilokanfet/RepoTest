using System;
using System.ServiceModel;

namespace EventService
{
    [ServiceContract]
    public interface IEventServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnSaveIncomingRequestPublishEvent(Guid requestId);

        [OperationContract(IsOneWay = true)]
        void OnSaveDirectumTaskPublishEvent(Guid taskId);
    }
}