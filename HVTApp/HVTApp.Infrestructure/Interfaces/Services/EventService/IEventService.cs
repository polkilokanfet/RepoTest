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

        /// <summary>
        /// ������ ��������
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool HostIsAlive();

        /// <summary>
        /// ������������ ��������� � �������
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UserIsConnected(Guid userId);

        /// <summary>
        /// ����������� ���������� � �������
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="targetDirectory">���� ���������� ����������</param>
        /// <returns></returns>
        [OperationContract]
        bool CopyProjectAttachments(Guid userId, Guid projectId, string targetDirectory);

        #region Chat

        [OperationContract]
        void SendMessageToChat(Guid authorId, string message);

        [OperationContract]
        void SendMessageToUser(Guid authorId, Guid recipientId, string message);

        #endregion

        #region Directum

        [OperationContract(IsOneWay = true)]
        void SaveDirectumTaskPublishEvent(Guid appSessionId, Guid taskId);

        [OperationContract(IsOneWay = true)]
        void StartDirectumTaskPublishEvent(Guid appSessionId, Guid taskId);

        [OperationContract(IsOneWay = true)]
        void StopDirectumTaskPublishEvent(Guid appSessionId, Guid taskId);

        [OperationContract(IsOneWay = true)]
        void PerformDirectumTaskPublishEvent(Guid appSessionId, Guid taskId);

        [OperationContract(IsOneWay = true)]
        void AcceptDirectumTaskPublishEvent(Guid appSessionId, Guid taskId);

        [OperationContract(IsOneWay = true)]
        void RejectDirectumTaskPublishEvent(Guid appSessionId, Guid taskId);

        #endregion

        #region PriceCalculation

        [OperationContract(IsOneWay = true)]
        void SavePriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void StartPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void FinishPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void CancelPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        [OperationContract(IsOneWay = true)]
        void RejectPriceCalculationPublishEvent(Guid appSessionId, Guid priceCalculationId);

        #endregion

        [OperationContract(IsOneWay = true)]
        void SaveIncomingRequestPublishEvent(Guid appSessionId, Guid requestId);

        [OperationContract(IsOneWay = true)]
        void SaveIncomingDocumentPublishEvent(Guid appSessionId, Guid documentId);

        #region TechnicalRequarementsTask

        [OperationContract(IsOneWay = true)]
        void SaveTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void StartTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void InstructTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void StopTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void RejectTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void FinishTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void AcceptTechnicalRequarementsTaskPublishEvent(Guid appSessionId, Guid technicalRequarementsTaskId);

        #endregion
    }
}