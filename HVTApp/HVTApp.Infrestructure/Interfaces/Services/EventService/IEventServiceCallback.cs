using System;
using System.Collections.Generic;
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
        /// <param name="projectId">Id проекта</param>
        /// <param name="targetDirectory">Куда копировать</param>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void CopyProjectAttachmentsCallback(Guid projectId, string targetDirectory);

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

        [OperationContract(IsOneWay = true)]
        void OnSaveIncomingRequestServiceCallback(Guid requestId);

        [OperationContract(IsOneWay = true)]
        void OnSaveIncomingDocumentServiceCallback(Guid documentId);

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

        /// <summary>
        /// Проверить сообщения, сохраненные в базе данных
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void CheckMessagesInDb();
    }
}