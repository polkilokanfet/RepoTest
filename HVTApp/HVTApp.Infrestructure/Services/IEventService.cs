using System;
using System.ServiceModel;

namespace HVTApp.Infrastructure.Services
{
    //[ServiceContract(CallbackContract = typeof(IEventServiceCallback))]
    //public interface IEventService
    //{
    //    [OperationContract]
    //    bool Connect(Guid userId);

    //    [OperationContract(IsOneWay = true)]
    //    void Disconnect(Guid userId);

    //    [OperationContract(IsOneWay = true)]
    //    void SaveItemPublishEvent(Guid userId, object item);

    //    [OperationContract(IsOneWay = true)]
    //    void RemoveItemPublishEvent(Guid userId, object item);

    //}

    //[ServiceContract]
    //public interface IEventServiceCallback
    //{
    //    [OperationContract(IsOneWay = true)]
    //    void OnSaveItemPublishEvent(object item);

    //    [OperationContract(IsOneWay = true)]
    //    void OnRemoveItemPublishEvent(object item);
    //}
}