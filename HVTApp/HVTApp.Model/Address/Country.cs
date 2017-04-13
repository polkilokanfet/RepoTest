using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    /// <summary>
    /// Страна.
    /// </summary>
    public class Country : BaseEntity
    {
        public int StandartDeliveryPeriod { get; set; }
        public string Name { get; set; }
        public virtual List<District> Districts { get; set; } // Округа.
    }
}