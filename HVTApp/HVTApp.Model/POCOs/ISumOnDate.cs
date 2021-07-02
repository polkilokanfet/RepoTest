using System;

namespace HVTApp.Model.POCOs
{
    public interface ISumOnDate
    {
        DateTime Date { get; set; }
        double Sum { get; set; }
    }
}