using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class SalesUnit : BaseEntity
    {
        public virtual ProductionUnit ProductionUnit { get; set; }
        public virtual ShipmentUnit ShipmentUnit { get; set; }

        public double Cost { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual List<PaymentCondition> PaymentsConditions { get; set; }

        public virtual List<PaymentActual> PaymentsActual { get; set; }
        public virtual List<PaymentPlanned> PaymentsPlanned { get; set; }

        public virtual DateTime? RealizationDate { get; set; }

        public virtual ProjectUnit ProjectUnit { get; set; }
        public virtual OfferUnit OfferUnit { get; set; }
    }
}