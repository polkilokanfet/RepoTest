namespace HVTApp.Infrastructure.Enums
{
    public enum NotificationActionType
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
        PriceEngineeringTaskInstructToConstructor,
        PriceEngineeringTaskFinish,
        PriceEngineeringTaskAccept,
        PriceEngineeringTaskRejectByManager,
        PriceEngineeringTaskRejectByConstructorToManager,
        PriceEngineeringTaskSendMessage,
        PriceEngineeringTaskFinishGoToVerification, 
        PriceEngineeringTaskVerificationRejectedByHead,
        PriceEngineeringTaskVerificationAcceptedByHead,

        SavePaymentDocument,

        PriceEngineeringTaskNotification,

        PriceEngineeringTaskInstructToPlanMaker,
        PriceEngineeringTaskLoadToTceStart,
        PriceEngineeringTaskLoadToTceFinish,
        PriceEngineeringTaskProductionRequestStart,
        PriceEngineeringTaskProductionRequestFinish,
        PriceEngineeringTaskProductionRequestStop,
        PriceEngineeringTaskProductionRequestStopConfirm,
        PriceEngineeringTaskProductionRequestStopReject,
        PriceEngineeringTaskRejectByHeadToManager,
        

        PriceEngineeringTasksInstructToBackManager,
    }
}