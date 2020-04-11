using System;
using System.ServiceModel;

namespace EventService
{
    [ServiceContract(CallbackContract = typeof(IEventServiceCallback))]
    public interface IEventService
    {
        [OperationContract]
        bool Connect(Guid appSassionId);

        [OperationContract(IsOneWay = true)]
        void Disconnect(Guid appSassionId);

        [OperationContract(IsOneWay = true)]
        void SaveIncomingRequestPublishEvent(Guid appSassionId, Guid requestId);
    }
}