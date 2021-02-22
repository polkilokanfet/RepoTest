using System;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{
    /// <summary>
    /// �������, ����������� ����� ������ ������� ��
    /// </summary>
    public class AfterStartPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

    /// <summary>
    /// �������, ����������� ����� ������ ������� ��
    /// </summary>
    public class AfterFinishPriceCalculationEvent : PubSubEvent<PriceCalculation> { }
    
    /// <summary>
    /// �������, ����������� ����� ��������� ������� ��
    /// </summary>
    public class AfterCancelPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

}