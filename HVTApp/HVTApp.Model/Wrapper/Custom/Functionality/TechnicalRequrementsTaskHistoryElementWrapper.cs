using System;

namespace HVTApp.Model.Wrapper
{
    public partial class TechnicalRequrementsTaskHistoryElementWrapper : IHistoryElementWrapper
    {

    }

    public interface IHistoryElementWrapper
    {
        DateTime Moment { get; }
        string Comment { get; }
    }
}