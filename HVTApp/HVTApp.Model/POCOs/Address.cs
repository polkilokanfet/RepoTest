using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Address : BaseEntity
    {
        public string Description { get; set; }
        public virtual Locality Locality { get; set; }

        public override string ToString()
        {
            return Locality.ToString() + "; " + Description;
        }

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

        public override string ToString()
        {
            return LocalityType.ShortName + Name;
        }
    }

    /// <summary>
    /// Тип населенного пункта.
    /// </summary>
    public class LocalityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, ({ShortName})";
        }
    }

    /// <summary>
    /// Область, край, республика и т.д.
    /// </summary>
    public class Region : BaseEntity
    {
        public string Name { get; set; }
        public virtual District District { get; set; }
        public virtual List<Locality> Localities { get; set; } // Населенные пункты.

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Округ страны.
    /// </summary>
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public virtual Country Country { get; set; }
        public virtual List<Region> Regions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Страна
    /// </summary>
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<District> Districts { get; set; } // Округа.
        public override string ToString()
        {
            return Name;
        }
    }
}