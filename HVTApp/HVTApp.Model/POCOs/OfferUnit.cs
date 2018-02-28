using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class OfferUnit : BaseEntity, IProductCostDependentProducts
    {
        public double Cost { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<ProductDependent> DependentProducts { get; set; } = new List<ProductDependent>();

        public virtual Facility Facility { get; set; }
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }
        public int? ProductionTerm { get; set; }
    }

    public interface IProductCostDependentProducts : IProductCost
    {
        List<ProductDependent> DependentProducts { get; }
    }

    public interface IProductCost : IBaseEntity
    {
        Product Product { get; set; }
        double Cost { get; set; }
    }

}