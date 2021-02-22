using System;
using System.ServiceModel;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    [ServiceContract(CallbackContract = typeof(IEventServiceCallback))]
    public interface IEventService
    {
        /// <summary>
        /// ����������� � �������
        /// </summary>
        /// <param name="appSessionId">Id ������ ����������</param>
        /// <param name="userId">Id �����</param>
        /// <returns></returns>
        [OperationContract]
        bool Connect(Guid appSessionId, Guid userId);

        /// <summary>
        /// ���������� �� �������
        /// </summary>
        /// <param name="appSessionId">Id ������ ����������</param>
        [OperationContract]
        void Disconnect(Guid appSessionId);


        [OperationContract]
        void SendMessageToChat(Guid authorId, string message);

        [OperationContract]
        void SendMessageToUser(Guid authorId, Guid recipientId, string message);


        [OperationContract(IsOneWay = true)]
        void SaveDirectumTaskPublishEvent(Guid appSessionId, Guid taskId);

        [OperationContract(IsOneWay = true)]
        void SavePriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void StartPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void FinishPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void CancelPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void SaveIncomingRequestPublishEvent(Guid appSessionId, Guid requestId);

        [OperationContract(IsOneWay = true)]
        void SaveIncomingDocumentPublishEvent(Guid appSessionId, Guid documentId);

        [OperationContract(IsOneWay = true)]
        void SaveTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);
    }
}