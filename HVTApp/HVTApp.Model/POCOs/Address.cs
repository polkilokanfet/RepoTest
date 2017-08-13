using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Address : BaseEntity
    {
        public string Description { get; set; }
        public virtual Locality Locality { get; set; }
    }

    /// <summary>
    /// Населенный пункт.
    /// </summary>
    public class Locality : BaseEntity
    {
        public string Name { get; set; }
        public virtual LocalityType LocalityType { get; set; }
        public virtual Region Region { get; set; }
        public virtual bool IsRegionCapital { get; set; } = false;
        public virtual bool IsDistrictsCapital { get; set; } = false;
        public virtual bool IsCountryCapital { get; set; } = false;

        public double? StandartDeliveryPeriod { get; set; }
    }

    /// <summary>
    /// Тип населенного пункта.
    /// </summary>
    public class LocalityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }

    /// <summary>
    /// Область, край, республика и т.д.
    /// </summary>
    public class Region : BaseEntity
    {
        public string Name { get; set; }
        public virtual District District { get; set; }
        public virtual Locality Capital => Localities.SingleOrDefault(x => x.IsRegionCapital);
        public virtual List<Locality> Localities { get; set; } // Населенные пункты.
    }

    /// <summary>
    /// Округ страны.
    /// </summary>
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public virtual Locality Capital => Regions.SelectMany(x => x.Localities).SingleOrDefault(x => x.IsDistrictsCapital);
        public virtual Country Country { get; set; }
        public virtual List<Region> Regions { get; set; }
    }

    /// <summary>
    /// Страна
    /// </summary>
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public virtual Locality Capital => Districts.SelectMany(x => x.Regions).SelectMany(x => x.Localities).SingleOrDefault(x => x.IsCountryCapital);
        public virtual List<District> Districts { get; set; } // Округа.
    }
}