using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;

namespace EventServiceClient2
{
    //Тут собраны запросы к хосту
    public partial class EventServiceClient : IEventServiceClient
    {
        //public bool SaveDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        //{
        //    return this.EventServiceHost.SaveDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        //}

        //public bool StartDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        //{
        //    return this.EventServiceHost.StartDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        //}

        //public bool StopDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        //{
        //    return this.EventServiceHost.StopDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        //}

        //public bool PerformDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        //{
        //    return this.EventServiceHost.PerformDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        //}

        //public bool AcceptDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        //{
        //    return this.EventServiceHost.AcceptDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        //}

        //public bool RejectDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        //{
        //    return this.EventServiceHost.RejectDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        //}

        //public bool SavePriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        //{
        //    return this.EventServiceHost.SavePriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        //}

        //public bool StartPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        //{
        //    return this.EventServiceHost.StartPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        //}

        //public bool FinishPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        //{
        //    return this.EventServiceHost.FinishPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        //}

        //public bool CancelPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        //{
        //    return this.EventServiceHost.CancelPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        //}

        //public bool RejectPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        //{
        //    return this.EventServiceHost.RejectPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        //}

        //public bool SaveIncomingRequestPublishEvent(Guid targetUserId, Role targetRole, Guid requestId)
        //{
        //    return this.EventServiceHost.SaveIncomingRequestPublishEvent(_appSessionId, targetUserId, targetRole, requestId);
        //}

        //public bool SaveIncomingDocumentPublishEvent(Guid targetUserId, Role targetRole, Guid documentId)
        //{
        //    return this.EventServiceHost.SaveIncomingDocumentPublishEvent(_appSessionId, targetUserId, targetRole, documentId);
        //}

        //public bool SaveTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.SaveTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool StartTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.StartTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool InstructTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole,
        //    Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.InstructTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool StopTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.StopTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool RejectTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.RejectTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool FinishTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.FinishTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool AcceptTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        //{
        //    return this.EventServiceHost.AcceptTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        //}

        //public bool SavePaymentDocumentPublishEvent(Guid targetUserId, Role targetRole, Guid paymentDocumentId)
        //{
        //    return this.EventServiceHost.SavePaymentDocumentPublishEvent(_appSessionId, targetUserId, targetRole, paymentDocumentId);
        //}

        //public bool PriceEngineeringTaskNotificationEvent(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, string message)
        //{
        //    return this.EventServiceHost.NotificationEvent(_appSessionId, userAuthorId, userTargetId, userTargetRole, priceEngineeringTaskId, message);
        //}

        public bool PriceEngineeringTaskSendMessagePublishEvent(Guid targetUserId, Role targetRole, Guid messageId)
        {
            return this.EventServiceHost.PriceEngineeringTaskSendMessagePublishEvent(_appSessionId, targetUserId, targetRole, messageId);
        }
    }
}