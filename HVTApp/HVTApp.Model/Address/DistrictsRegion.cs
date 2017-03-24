using System.Collections.Generic;

namespace HVTApp.Model
{
    /// <summary>
    /// Область, край, республика и т.д.
    /// </summary>
    public class DistrictsRegion : BaseEntity
    {
        public string Name { get; set; }
        public virtual District District { get; set; }
        /// <summary>
        /// Населенные пункты.
        /// </summary>
        public virtual List<Locality> Localities { get; set; }
    }
}