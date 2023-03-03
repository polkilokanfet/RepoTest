using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{
    /// <summary>
    /// Событие, поднимаемое после старта расчета ПЗ
    /// </summary>
    public class AfterStartPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

    /// <summary>
    /// Событие, поднимаемое после финиша расчета ПЗ
    /// </summary>
    public class AfterFinishPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

    /// <summary>
    /// Событие, поднимаемое после отклонения расчета ПЗ
    /// </summary>
    public class AfterRejectPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

    /// <summary>
    /// Событие, поднимаемое после остановки расчета ПЗ
    /// </summary>
    public class AfterStopPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

    #region Directum

    public class AfterStartDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterStopDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterPerformDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterAcceptDirectumTaskEvent : PubSubEvent<DirectumTask> { }
    public class AfterRejectDirectumTaskEvent : PubSubEvent<DirectumTask> { }

    #endregion

    #region TechnicalRequrementsTask

    public class AfterStartTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterInstructTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterRejectTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterRejectByFrontManagerTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterStopTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterFinishTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterAcceptTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }

    #endregion

    #region PriceEngineeringTasks

    public class PriceEngineeringTasksStartedEvent : PubSubEvent<PriceEngineeringTasks> { }

    public class PriceEngineeringTaskStartedEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskStoppedEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskInstructedEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskFinishedEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskAcceptedEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskRejectedByManagerEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskRejectedByHeadEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskRejectedByConstructorEvent : PubSubEvent<PriceEngineeringTask> { }

    public class PriceEngineeringTaskFinishedGoToVerificationEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskVerificationRejectedByHeadEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskVerificationAcceptedByHeadEvent : PubSubEvent<PriceEngineeringTask> { }

    public class PriceEngineeringTaskLoadToTceStartEvent : PubSubEvent<PriceEngineeringTask> { }
    public class PriceEngineeringTaskLoadToTceFinishEvent : PubSubEvent<PriceEngineeringTask> { }



    public class PriceEngineeringTaskSendMessageEvent : PubSubEvent<PriceEngineeringTaskMessage> { }
    public class PriceEngineeringTaskReciveMessageEvent : PubSubEvent<PriceEngineeringTaskMessage> { }

    #endregion

    //#region ActualPayment

    public class AfterSaveActualPaymentDocumentEvent : PubSubEvent<PaymentDocument> { }

    //public class AfterSaveActualPaymentDocumentEvent : PubSubEvent<ActualPaymentEventEntity> { }
    //public class AfterSaveActualPaymentEvent : PubSubEvent<SalesUnit> { }

    //#endregion

    ///// <summary>
    ///// Контейнер для передачи информации при синхронизации поступивших платежей.
    ///// </summary>
    //public class ActualPaymentEventEntity
    //{
    //    public PaymentDocument PaymentDocument { get; }
    //    public IEnumerable<SalesUnit> SalesUnits { get; }

    //    public ActualPaymentEventEntity(PaymentDocument paymentDocument, IEnumerable<SalesUnit> salesUnits)
    //    {
    //        PaymentDocument = paymentDocument;
    //        SalesUnits = salesUnits;
    //    }

    //    public override string ToString()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append(PaymentDocument);
    //        foreach (var salesUnit in SalesUnits)
    //        {
    //            sb.AppendLine($"за {salesUnit}");
    //            foreach (var payment in PaymentDocument.Payments.Where(paymentActual => salesUnit.PaymentsActual.ContainsById(paymentActual)))
    //            {
    //                sb.AppendLine($" - {payment}");
    //            }
    //        }

    //        return sb.ToString();
    //    }
    //}
}