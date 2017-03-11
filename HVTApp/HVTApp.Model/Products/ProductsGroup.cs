using System;
using System.Linq;

namespace HVTApp.Model
{
    public abstract class ProductsGroup<TProduct> : BaseEntity
        where TProduct : ProductBase
    {
        public virtual ProductsBaseCollection<TProduct> Products { get; set; }
        public double Sum => Products.Sum(x => x.CostInfo.Cost);
        public double SumWithVat => Products.Sum(x => x.CostInfo.CostWithVat);
        public DateTime OrderInTakeDate => Products.Select(x => x.DateInfo.DateOrderInTakeCalculated).Min();
    }
}