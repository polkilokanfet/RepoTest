using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    [ServiceContract(CallbackContract = typeof(IEventServiceCallback))]
    public interface IEventService
    {
        /// <summary>
        /// Подключение к сервису
        /// </summary>
        /// <param name="appSessionId">Id сессии приложения</param>
        /// <param name="userId">Id юзера</param>
        /// <returns></returns>
        [OperationContract]
        bool Connect(Guid appSessionId, Guid userId);

        /// <summary>
        /// Отключение от сервиса
        /// </summary>
        /// <param name="appSessionId">Id сессии приложения</param>
        [OperationContract]
        void Disconnect(Guid appSessionId);

        /// <summary>
        /// Сервис доступен
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool HostIsAlive();

        /// <summary>
        /// Пользователь подключен к сервису
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UserIsConnected(Guid userId);

        /// <summary>
        /// Скопировать приложения к проекту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="targetDirectory">Куда копировать приложения</param>
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

        [OperationContract]
        bool SaveDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId);

        [OperationContract]
        bool StartDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId);

        [OperationContract]
        bool StopDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId);

        [OperationContract]
        bool PerformDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId);

        [OperationContract]
        bool AcceptDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId);

        [OperationContract]
        bool RejectDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid taskId);

        #endregion

        #region PriceCalculation

        [OperationContract]
        bool SavePriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId);

        /// <summary>
        /// Публикация события старта расчета ПЗ
        /// </summary>
        /// <param name="eventSourceAppSessionId">Id приложения, которое инициировало событие</param>
        /// <param name="targetUserId">Id пользователя, которому необходимо доставить уведомление</param>
        /// <param name="priceCalculationId">Id расчета ПЗ</param>
        /// <returns>Доставлено ли уведомление целевому пользователю</returns>
        [OperationContract]
        bool StartPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId);

        [OperationContract]
        bool FinishPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId);

        [OperationContract]
        bool CancelPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId);

        [OperationContract]
        bool RejectPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceCalculationId);

        #endregion

        #region Incoming

        [OperationContract]
        bool SaveIncomingRequestPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid requestId);

        [OperationContract]
        bool SaveIncomingDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid documentId);

        #endregion

        #region TechnicalRequarementsTask

        [OperationContract]
        bool SaveTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool StartTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool InstructTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool StopTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool RejectTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool FinishTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool AcceptTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid technicalRequarementsTaskId);

        #endregion

        #region PriceEngineeringTasks

        [OperationContract]
        bool PriceEngineeringTasksStartPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTasksId);


        [OperationContract]
        bool PriceEngineeringTaskStartPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTaskId);

        [OperationContract]
        bool PriceEngineeringTaskInstructPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTaskId);

        [OperationContract]
        bool PriceEngineeringTaskFinishPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTaskId);

        [OperationContract]
        bool PriceEngineeringTaskAcceptPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTaskId);

        [OperationContract]
        bool PriceEngineeringTaskRejectByManagerPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTaskId);

        [OperationContract]
        bool PriceEngineeringTaskRejectByConstructorPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTaskId);

        [OperationContract]
        bool PriceEngineeringTaskStopPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Guid priceEngineeringTaskId);

        #endregion
    }
}