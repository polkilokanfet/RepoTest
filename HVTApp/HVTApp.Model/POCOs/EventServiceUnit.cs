using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("EventServiceUnit")]
    public partial class EventServiceUnit : BaseEntity
    {
        [Designation("Пользователь"), Required, OrderStatus(10)]
        public virtual User User { get; set; }

        [Designation("Роль"), OrderStatus(8)]
        public virtual Role? Role { get; set; }

        [Designation("Id целевой сущности"), Required, OrderStatus(5)]
        public virtual Guid TargetEntityId { get; set; }

        [Designation("Тип действия"), Required, OrderStatus(7)]
        public EventServiceActionType EventServiceActionType { get; set; }

        [Designation("Сообщение"), OrderStatus(3)]
        public string Message { get; set; }
    }

    public enum EventServiceActionType
    {
        SavePriceCalculation,
        StartPriceCalculation,
        CancelPriceCalculation,
        RejectPriceCalculation,
        FinishPriceCalculation,

        SaveTechnicalRequrementsTask,
        StartTechnicalRequrementsTask,
        InstructTechnicalRequrementsTask,
        RejectTechnicalRequrementsTask,
        RejectByFrontManagerTechnicalRequrementsTask,
        FinishTechnicalRequrementsTask,
        AcceptTechnicalRequrementsTask,
        StopTechnicalRequrementsTask,

        SaveDirectumTask,
        StartDirectumTask,
        StopDirectumTask,
        PerformDirectumTask,
        AcceptDirectumTask,
        RejectDirectumTask,

        SaveIncomingRequest,

        SaveActualPayment,

        PriceEngineeringTasksStart,

        PriceEngineeringTaskStart,
        PriceEngineeringTaskStop,
        PriceEngineeringTaskInstruct,
        PriceEngineeringTaskFinish,
        PriceEngineeringTaskAccept,
        PriceEngineeringTaskRejectByManager,
        PriceEngineeringTaskRejectByConstructor,
        PriceEngineeringTaskSendMessage,
        PriceEngineeringTaskFinishGoToVerification, 
        PriceEngineeringTaskVerificationRejectedByHead,
        PriceEngineeringTaskVerificationAcceptedByHead,

        SavePaymentDocument,

        PriceEngineeringTaskNotification
    }
}