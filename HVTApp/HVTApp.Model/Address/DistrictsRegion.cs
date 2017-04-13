using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    /// <summary>
    /// Область, край, республика и т.д.
    /// </summary>
    public class DistrictsRegion : BaseEntity
    {
        public int StandartDeliveryPeriod { get; set; }
        public string Name { get; set; }
        public virtual District District { get; set; }
        public virtual List<Locality> Localities { get; set; } // Населенные пункты.
    }
}