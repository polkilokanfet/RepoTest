using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Населенный пункт.
    /// </summary>
    public class Locality : BaseEntity
    {
        public string Name { get; set; }
        public virtual LocalityType LocalityType { get; set; }
        public virtual Guid RegionId { get; set; }
        public virtual bool IsRegionCapital { get; set; } = false;
        public virtual bool IsDistrictsCapital { get; set; } = false;
        public virtual bool IsCountryCapital { get; set; } = false;

        public double? StandartDeliveryPeriod { get; set; }

        public override string ToString()
        {
            return LocalityType.ShortName + Name;
        }
    }
}