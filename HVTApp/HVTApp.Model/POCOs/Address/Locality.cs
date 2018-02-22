using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Населенный пункт.
    /// </summary>
    public partial class Locality : BaseEntity
    {
        public string Name { get; set; }
        public virtual LocalityType LocalityType { get; set; }
        public virtual Region Region { get; set; }

        public double? StandartDeliveryPeriod { get; set; }

        public override string ToString()
        {
            return $"{Region}, {LocalityType.ShortName} {Name}";
        }
    }
}