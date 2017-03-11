using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model
{
    public class OfferUnit : BaseEntity
    {
        public virtual Equipment Equipment { get; set; }
        public int Count => OfferProducts.Count;
        public virtual List<Facility> Facilities => OfferProducts.Select(x => x.ProductMain.ProductsMainGroup.Facility).Distinct().ToList();
        public virtual List<OfferProduct> OfferProducts { get; set; } = new List<OfferProduct>();
        public virtual Offer Offer { get; set; }
        public virtual PlannedTermProduction PlannedTermProduction { get; set; }

        public double Sum => OfferProducts.Sum(x => x.CostInfo.Cost);
        public double SumWithVat => OfferProducts.Sum(x => x.CostInfo.CostWithVat);
    }
}