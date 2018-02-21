using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class CalculatePriceTask : BaseEntity
    {
        public DateTime PriceOnDate { get; set; }
        public virtual ProductBlock ProductBlock { get; set; }
        public bool IsActual { get; set; } = true;
        public List<Guid> RequestMakers { get; set; } = new List<Guid>();
    }
}