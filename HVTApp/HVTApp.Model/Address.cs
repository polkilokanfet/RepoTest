﻿using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
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
        public virtual StandartDeliveryPeriod DeliveryPeriod { get; set; }
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
        public virtual Locality Capital { get; set; }
        public virtual List<Locality> Localities { get; set; } // Населенные пункты.
    }

    /// <summary>
    /// Округ страны.
    /// </summary>
    public class District : BaseEntity
    {
        public int StandartDeliveryPeriod { get; set; }
        public string Name { get; set; }
        public virtual Locality Capital { get; set; }
        public virtual Country Country { get; set; }
    }

    /// <summary>
    /// Страна.
    /// </summary>
    public class Country : BaseEntity
    {
        public int StandartDeliveryPeriod { get; set; }
        public string Name { get; set; }
        public virtual Locality Capital { get; set; }
        public virtual List<District> Districts { get; set; } // Округа.
    }

    /// <summary>
    /// стандартное время доставки чего-либо из Екатеринбурга
    /// </summary>
    public class StandartDeliveryPeriod : BaseEntity
    {
        public virtual Locality Locality { get; set; }
        public int DeliveryPeriod { get; set; }
    }
}