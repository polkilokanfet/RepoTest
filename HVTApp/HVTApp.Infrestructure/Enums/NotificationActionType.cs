namespace HVTApp.Infrastructure.Enums
{
    public enum NotificationActionType
    {
        SavePriceCalculation,
        StartPriceCalculation,
        CancelPriceCalculation,
        RejectPriceCalculation,
        FinishPriceCalculation,

        SaveTechnicalRequirementsTask,
        StartTechnicalRequirementsTask,
        InstructTechnicalRequirementsTask,
        RejectTechnicalRequirementsTask,
        RejectByFrontManagerTechnicalRequirementsTask,
        FinishTechnicalRequirementsTask,
        AcceptTechnicalRequirementsTask,
        StopTechnicalRequirementsTask,

        //SaveDirectumTask,
        //StartDirectumTask,
        //StopDirectumTask,
        //PerformDirectumTask,
        //AcceptDirectumTask,
        //RejectDirectumTask,

        //SaveIncomingRequest,

        //SaveActualPayment,

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

        //SavePaymentDocument,

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