using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Область, край, республика и т.д.
    /// </summary>
    public partial class Region : BaseEntity
    {
        public string Name { get; set; }
        public virtual Guid DistrictId { get; set; }
        public virtual List<Locality> Localities { get; set; } // Населенные пункты.

        public override string ToString()
        {
            return Name;
        }
    }
}