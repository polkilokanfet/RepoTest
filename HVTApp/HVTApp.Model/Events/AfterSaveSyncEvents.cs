using System;
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
    /// Событие, поднимаемое после остановки расчета ПЗ
    /// </summary>
    public class AfterCancelPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

}