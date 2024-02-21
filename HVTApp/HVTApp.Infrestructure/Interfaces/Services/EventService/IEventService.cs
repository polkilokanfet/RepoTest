using System;
using System.ServiceModel;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    [ServiceContract(CallbackContract = typeof(IEventServiceCallback))]
    public partial interface IEventService
    {
        /// <summary>
        /// Подключение к сервису
        /// </summary>
        /// <param name="appSessionId">Id сессии приложения</param>
        /// <param name="userId">Id пользователя</param>
        /// <param name="userRole">Роль пользователя</param>
        /// <returns></returns>
        [OperationContract]
        bool Connect(Guid appSessionId, Guid userId, Role userRole);

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

        #region Chat

        [OperationContract]
        void SendMessageToChat(Guid authorId, string message);

        [OperationContract]
        void SendMessageToUser(Guid authorId, Guid recipientId, string message);

        #endregion

        #region Directum

        [OperationContract]
        bool SaveDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        [OperationContract]
        bool StartDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        [OperationContract]
        bool StopDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        [OperationContract]
        bool PerformDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        [OperationContract]
        bool AcceptDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        [OperationContract]
        bool RejectDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        #endregion

        #region PriceCalculation

        [OperationContract]
        bool SavePriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        /// <summary>
        /// Публикация события старта расчета ПЗ
        /// </summary>
        /// <param name="eventSourceAppSessionId">Id приложения, которое инициировало событие</param>
        /// <param name="targetUserId">Id пользователя, которому необходимо доставить уведомление</param>
        /// <param name="priceCalculationId">Id расчета ПЗ</param>
        /// <returns>Доставлено ли уведомление целевому пользователю</returns>
        [OperationContract]
        bool StartPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        [OperationContract]
        bool FinishPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        [OperationContract]
        bool CancelPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        [OperationContract]
        bool RejectPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        #endregion

        #region Incoming

        [OperationContract]
        bool SaveIncomingRequestPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid requestId);

        [OperationContract]
        bool SaveIncomingDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid documentId);

        #endregion

        #region TechnicalRequarementsTask

        [OperationContract]
        bool SaveTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool StartTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool InstructTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool StopTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool RejectTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool FinishTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        [OperationContract]
        bool AcceptTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        #endregion

        [OperationContract]
        bool SavePaymentDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid paymentDocumentId);
    }

    public partial interface IEventService
    {
        [OperationContract]
        bool PriceEngineeringTaskNotificationEvent(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, string message);
        [OperationContract]
        bool PriceEngineeringTaskSendMessagePublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid messageId);
    }
}