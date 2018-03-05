using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class OfferUnit : BaseEntity, IUnit
    {
        public double Cost { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<ProductDependent> DependentProducts { get; set; } = new List<ProductDependent>();
        public virtual List<Service> Services { get; set; } = new List<Service>();

        public virtual Facility Facility { get; set; }
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }
        public int? ProductionTerm { get; set; }
    }

    public interface IUnit : IProductCost
    {
        List<ProductDependent> DependentProducts { get; }
        List<Service> Services { get; }
    }

    public interface IProductCost : IBaseEntity
    {
        Product Product { get; set; }
        double Cost { get; set; }
    }

}