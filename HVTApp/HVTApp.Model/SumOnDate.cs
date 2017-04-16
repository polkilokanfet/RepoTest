using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    /// <summary>
    /// Сумма на какую-либо дату
    /// </summary>
    public class SumOnDate : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Sum { get; set; }
    }
}