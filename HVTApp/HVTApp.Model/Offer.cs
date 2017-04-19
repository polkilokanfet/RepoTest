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
        public virtual List<OfferUnit> OfferUnits { get; set; }
    }

    public class OfferUnit : BaseEntity
    {
        public virtual Offer Offer { get; set; }
        public virtual SalesUnit SalesUnit { get; set; }

        public virtual OfferUnit ParentOfferUnit { get; set; }
        public virtual List<OfferUnit> ChildOfferUnits { get; set; } = new List<OfferUnit>();

        public virtual Facility Facility { get; set; }
        public virtual SumAndVat CostSingle { get; set; }

        public virtual ProductionUnit ProductionUnit { get; set; }
        public virtual ShipmentUnit ShipmentUnit { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
    }

}