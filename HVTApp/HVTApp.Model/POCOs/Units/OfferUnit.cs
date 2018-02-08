using System;
using System.Collections.Generic;
using System.Linq;
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

    public partial class OfferUnitGroup : BaseEntity
    {
        private OfferUnit OfferUnit => OfferUnits.FirstOrDefault();

        public virtual Offer Offer => OfferUnit?.Offer;
        public virtual Product Product => OfferUnit?.Product;
        public virtual Facility Facility => OfferUnit?.Facility;
        public virtual int? ProductionTerm => OfferUnit?.ProductionTerm;
        public double Cost => OfferUnit.Cost;
        public int Amount => OfferUnits.Count;

        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();
    }

}
