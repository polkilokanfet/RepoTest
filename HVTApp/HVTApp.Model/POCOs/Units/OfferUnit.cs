using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class OfferUnit : BaseEntity
    {
        public virtual Offer Offer { get; set; }

        public virtual ProjectUnit ProjectUnit { get; set; }

        public virtual Facility Facility { get; set; }

        public virtual Product Product { get; set; }
        public double Cost { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public int ProductionTerm { get; set; } //срок производства

        public override string ToString()
        {
            return "OfferUnit: " + Product.ToString();
        }
    }
}
