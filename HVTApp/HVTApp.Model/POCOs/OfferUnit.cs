using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class OfferUnit : BaseEntity, IProductCostUnit
    {
        public double Cost { get; set; }
        public Product Product { get; set; }
        public List<Product> DependentProducts { get; set; }

        public virtual Facility Facility { get; set; }
    }

    public interface IProductCostUnit : IBaseEntity
    {
        Product Product { get; set; }
        List<Product> DependentProducts { get; }
        double Cost { get; set; }
    }

}