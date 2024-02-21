using System;
using System.ServiceModel;
using HVTApp.Infrastructure.Enums;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    [ServiceContract]
    public interface IEventServiceCallback
    {
        /// <summary>
        /// Закрыть приложение у пользователя
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void ApplicationShutdown();

        /// <summary>
        /// Реакция клиента на остановку сервиса
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void OnServiceDisposeEvent();

        [OperationContract(IsOneWay = true)]
        void OnSendMessageToChat(Guid authorId, string message);

        [OperationContract(IsOneWay = true)]
        void OnSendMessageToUser(Guid authorId, string message);

        #region Directum

        [OperationContract]
        bool OnSaveDirectumTaskServiceCallback(Guid taskId);

        [OperationContract]
        bool OnStartDirectumTaskServiceCallback(Guid taskId);

        [OperationContract]
        bool OnStopDirectumTaskServiceCallback(Guid taskId);

        [OperationContract]
        bool OnPerformDirectumTaskServiceCallback(Guid taskId);

        [OperationContract]
        bool OnAcceptDirectumTaskServiceCallback(Guid taskId);

        [OperationContract]
        bool OnRejectDirectumTaskServiceCallback(Guid taskId);

        #endregion

        #region PriceEngineeringTask

        [OperationContract]
        bool OnPriceEngineeringNotificationServiceCallback(string message, NotificationActionType actionType, Guid targetEntityId);


        [OperationContract]
        bool OnPriceEngineeringTaskSendMessageServiceCallback(Guid messageId);

        #endregion

        [OperationContract]
        bool OnSavePaymentDocumentServiceCallback(Guid paymentDocumentId);

        #region PriceCalculation

        [OperationContract]
        bool OnSavePriceCalculationServiceCallback(Guid calculationId);

        [OperationContract]
        bool OnStartPriceCalculationServiceCallback(Guid calculationId);

        [OperationContract]
        bool OnFinishPriceCalculationServiceCallback(Guid calculationId);

        [OperationContract]
        bool OnCancelPriceCalculationServiceCallback(Guid calculationId);
        
        [OperationContract]
        bool OnRejectPriceCalculationServiceCallback(Guid calculationId);

        #endregion

        [OperationContract]
        bool OnSaveIncomingRequestServiceCallback(Guid requestId);

        [OperationContract]
        bool OnSaveIncomingDocumentServiceCallback(Guid documentId);

        #region TechnicalRequarementsTask

        [OperationContract]
        bool OnSaveTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract]
        bool OnStartTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract]
        bool OnInstructTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract]
        bool OnStopTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract]
        bool OnRejectTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract]
        bool OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract]
        bool OnFinishTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        [OperationContract]
        bool OnAcceptTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId);

        #endregion

        /// <summary>
        /// Проверка: жив ли клиент
        /// </summary>
        /// <returns>жив?</returns>
        [OperationContract]
        bool IsAlive();
    }
}