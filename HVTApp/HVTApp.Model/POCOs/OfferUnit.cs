using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class OfferUnit : BaseEntity, IProductCostUnit
    {
        public double Cost { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<Product> DependentProducts { get; set; } = new List<Product>();

        public virtual Facility Facility { get; set; }
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }
        public int? ProductionTerm { get; set; }
    }

    public interface IProductCostUnit : IBaseEntity
    {
        Product Product { get; set; }
        List<Product> DependentProducts { get; }
        double Cost { get; set; }
    }

}