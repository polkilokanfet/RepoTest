using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class CalculatePriceTask : BaseEntity
    {
        public DateTime PriceOnDate { get; set; }
        public virtual ProductBlock ProductBlock { get; set; }
        public bool IsActual { get; set; } = true;
    }
}