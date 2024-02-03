using System;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public interface IEventServiceClient : IEventServiceCallback
    {
        Guid AppSessionId { get; }
        void Start();
        void Stop();

        bool UserConnected(Guid userId);

        /// <summary>
        /// Скопировать приложения к проекту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="targetDirectory">Куда копировать</param>
        /// <returns></returns>
        void CopyProjectAttachmentsRequest(Guid userId, Guid projectId, string targetDirectory);

        #region MessageToChat

        void SendMessageToChat(string message);
        void SendMessageToUser(Guid recipientId, string message);

        #endregion

        #region Directum

        
        bool SaveDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        
        bool StartDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        
        bool StopDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        
        bool PerformDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        
        bool AcceptDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        
        bool RejectDirectumTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid taskId);

        #endregion

        #region PriceCalculation

        
        bool SavePriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        /// <summary>
        /// Публикация события старта расчета ПЗ
        /// </summary>
        /// <param name="eventSourceAppSessionId"></param>
        /// <param name="targetUserId">Id пользователя, которому необходимо доставить уведомление</param>
        /// <param name="targetRole"></param>
        /// <param name="priceCalculationId">Id расчета ПЗ</param>
        /// <returns>Доставлено ли уведомление целевому пользователю</returns>
        bool StartPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        
        bool FinishPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        
        bool CancelPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        
        bool RejectPriceCalculationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceCalculationId);

        #endregion

        #region Incoming

        
        bool SaveIncomingRequestPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid requestId);

        
        bool SaveIncomingDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid documentId);

        #endregion

        #region TechnicalRequarementsTask

        
        bool SaveTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool StartTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool InstructTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool StopTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool RejectTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool FinishTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool AcceptTechnicalRequarementsTaskPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        #endregion

        bool SavePaymentDocumentPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid paymentDocumentId);

        bool PriceEngineeringTaskNotificationEvent(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, string message);

        #region PriceEngineeringTasks

        
        bool PriceEngineeringTasksStartPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTasksId);


        
        bool PriceEngineeringTaskStartPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);

        
        bool PriceEngineeringTaskInstructPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);

        
        bool PriceEngineeringTaskFinishPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);


        
        bool PriceEngineeringTaskFinishGoToVerificationPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);

        
        bool PriceEngineeringTaskVerificationRejectedByHeadPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);

        
        bool PriceEngineeringTaskVerificationAcceptedByHeadPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);



        
        bool PriceEngineeringTaskAcceptPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);

        
        bool PriceEngineeringTaskRejectByManagerPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);

        
        bool PriceEngineeringTaskRejectByConstructorPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);

        
        bool PriceEngineeringTaskStopPublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId);


        
        bool PriceEngineeringTaskSendMessagePublishEvent(Guid eventSourceAppSessionId, Guid targetUserId, Role targetRole, Guid messageId);

        #endregion
    }
}