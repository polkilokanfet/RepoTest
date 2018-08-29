using System;

namespace HVTApp.Model.POCOs
{
    public interface IPayment
    {
        DateTime Date { get; }
        double Sum { get; }
    }
}