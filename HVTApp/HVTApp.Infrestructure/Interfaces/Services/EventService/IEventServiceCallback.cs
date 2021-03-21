using System;
using System.ServiceModel;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    [ServiceContract]
    public interface IEventServiceCallback
    {
        /// <summary>
        /// Реакция клиента на остановку сервиса
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void OnServiceDisposeEvent();

        /// <summary>
        /// Скопировать приложения к проекту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="targetDirectory">Куда копировать</param>
        /// <returns></returns>
        [OperationContract]
        bool CopyProjectAttachmentsCallback(Guid userId, Guid projectId, string targetDirectory);

        [OperationContract(IsOneWay = true)]
        void OnSendMessageToChat(Guid authorId, string message);

        [OperationContract(IsOneWay = true)]
        void OnSendMessageToUser(Guid authorId, string message);

        #region Directum

        [OperationContract(IsOneWay = true)]
        void OnSaveDirectumTaskServiceCallback(Guid taskId);

        [OperationContract(IsOneWay = true)]
        void OnStartDirectumTaskServiceCallback(Guid taskId);

        [OperationContract(IsOneWay = true)]
        void OnStopDirectumTaskServiceCallback(Guid taskId);

        [OperationContract(IsOneWay = true)]
        void OnPerformDirectumTaskServiceCallback(Guid taskId);

        [OperationContract(IsOneWay = true)]
        void OnAcceptDirectumTaskServiceCallback(Guid taskId);

        [OperationContract(IsOneWay = true)]
        void OnRejectDirectumTaskServiceCallback(Guid taskId);

        #endregion

        #region PriceCalculation

        [OperationContract(IsOneWay = true)]
        void OnSavePriceCalculationServiceCallback(Guid calculationId);

        [OperationContract(IsOneWay = true)]
        void OnStartPriceCalculationServiceCallback(Guid calculationId);

        [OperationContract(IsOneWay = true)]
        void OnFinishPriceCalculationServiceCallback(Guid calculationId);

        [OperationContract(IsOneWay = true)]
        void OnCancelPriceCalculationServiceCallback(Guid calculationId);

        #endregion

        [OperationContract(IsOneWay = true)]
        void OnSaveIncomingRequestServiceCallback(Guid requestId);

        [OperationContract(IsOneWay = true)]
        void OnSaveIncomingDocumentServiceCallback(Guid documentId);

        #region TechnicalRequarementsTask

        [OperationContract(IsOneWay = true)]
        void OnSaveTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void OnStartTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void OnInstructTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void OnCancelTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract(IsOneWay = true)]
        void OnRejectTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);
        
        #endregion

        [OperationContract]
        bool IsAlive();
    }
}