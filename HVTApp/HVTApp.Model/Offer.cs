using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Offer : BaseEntity
    {
        public virtual Document Document { get; set; }
        public virtual Project Project { get; set; }
        public virtual Tender Tender { get; set; }
        public DateTime ValidityDate { get; set; } // Дата до которой ТКП действительно.
        public virtual List<SalesUnit> SalesUnits { get; set; }
    }
}