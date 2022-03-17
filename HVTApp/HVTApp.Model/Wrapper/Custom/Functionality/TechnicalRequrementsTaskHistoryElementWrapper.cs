using System;

namespace HVTApp.Model.Wrapper
{
    public partial class TechnicalRequrementsTaskHistoryElementWrapper : IHistoryElementWrapper
    {

    }

    public partial class PriceCalculationHistoryItemWrapper : IHistoryElementWrapper
    {

    }

    public interface IHistoryElementWrapper
    {
        DateTime Moment { get; }
        string Comment { get; }
        UserWrapper User { get; }
    }
}