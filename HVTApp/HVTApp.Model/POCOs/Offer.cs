using System;
using System.Collections.Generic;

namespace HVTApp.Model.POCOs
{
    public class Offer : Document
    {
        public virtual Document Document { get; set; }
        public virtual Project Project { get; set; }
        public virtual Tender Tender { get; set; }
        public DateTime ValidityDate { get; set; } // Дата до которой ТКП действительно.
        public virtual List<OfferUnit> ProductOfferUnits { get; set; } = new List<OfferUnit>();
        public double Vat { get; set; }
    }
}