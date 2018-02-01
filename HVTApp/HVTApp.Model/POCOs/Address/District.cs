using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Округ страны.
    /// </summary>
    public partial class District : BaseEntity
    {
        public string Name { get; set; }
        public virtual Guid CountryId { get; set; }
        public virtual List<Region> Regions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}