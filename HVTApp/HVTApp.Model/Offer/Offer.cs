using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class Offer : BaseEntity
    {
        public virtual Document Document { get; set; }
        public virtual Project Project { get; set; }
        public virtual Tender Tender { get; set; }
        public virtual List<OfferUnit> OfferUnits { get; set; }
        /// <summary>
        /// Дата до которой ТКП действительно.
        /// </summary>
        public DateTime ValidityDate { get; set; }
        public virtual PlannedTermProduction PlannedTermProduction { get; set; }
    }
}