using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class SalesUnit : BaseEntity
    {
        public Guid OfferUnitId { get; set; }
        public virtual OfferUnit OfferUnit { get; set; }
        public virtual ProductionUnit ProductionUnit { get; set; }
        public virtual ShipmentUnit ShipmentUnit { get; set; }

        public double Cost { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();

        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();
        public virtual List<PaymentPlanned> PaymentsPlanned { get; set; } = new List<PaymentPlanned>();

        public virtual DateTime? RealizationDate { get; set; }

        public override string ToString()
        {
            return "SalesUnit: " + ProductionUnit.Product.ToString();
        }
    }
}