using System;

namespace HVTApp.Infrastructure.Interfaces.Services.EventService
{
    public interface IEventServiceClient : IEventServiceCallback
    {
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

        
        bool SaveDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId);

        
        bool StartDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId);

        
        bool StopDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId);

        
        bool PerformDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId);

        
        bool AcceptDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId);

        
        bool RejectDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId);

        #endregion

        #region PriceCalculation

        
        bool SavePriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId);

        /// <summary>
        /// Публикация события старта расчета ПЗ
        /// </summary>
        /// <param name="eventSourceAppSessionId"></param>
        /// <param name="targetUserId">Id пользователя, которому необходимо доставить уведомление</param>
        /// <param name="targetRole"></param>
        /// <param name="priceCalculationId">Id расчета ПЗ</param>
        /// <returns>Доставлено ли уведомление целевому пользователю</returns>
        bool StartPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId);

        
        bool FinishPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId);

        
        bool CancelPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId);

        
        bool RejectPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId);

        #endregion

        #region Incoming

        
        bool SaveIncomingRequestPublishEvent(Guid targetUserId, Role targetRole, Guid requestId);

        
        bool SaveIncomingDocumentPublishEvent(Guid targetUserId, Role targetRole, Guid documentId);

        #endregion

        #region TechnicalRequarementsTask

        
        bool SaveTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool StartTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool InstructTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool StopTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool RejectTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool FinishTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        
        bool AcceptTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId);

        #endregion

        bool SavePaymentDocumentPublishEvent(Guid targetUserId, Role targetRole, Guid paymentDocumentId);

        bool PriceEngineeringTaskNotificationEvent(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, string message);

        #region PriceEngineeringTasks

        bool PriceEngineeringTaskSendMessagePublishEvent(Guid targetUserId, Role targetRole, Guid messageId);

        #endregion
    }
}