using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сумма на какую-либо дату
    /// </summary>
    public partial class SumOnDate : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual Sum Sum { get; set; }
    }
}