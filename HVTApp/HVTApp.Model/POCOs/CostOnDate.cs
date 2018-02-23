using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сумма на какую-либо дату
    /// </summary>
    public partial class CostOnDate : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual double Cost { get; set; }
    }
}