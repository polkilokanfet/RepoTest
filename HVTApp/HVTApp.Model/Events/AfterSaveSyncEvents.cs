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
    /// �������, ����������� ����� ���������� ������� ��
    /// </summary>
    public class AfterRejectPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

    /// <summary>
    /// �������, ����������� ����� ��������� ������� ��
    /// </summary>
    public class AfterCancelPriceCalculationEvent : PubSubEvent<PriceCalculation> { }

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
    public class AfterStopTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterFinishTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class AfterAcceptTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask> { }

    #endregion
}