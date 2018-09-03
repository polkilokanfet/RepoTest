using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public partial class OfferUnitLookup : IUnitLookup
    {
        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            DependentProducts = Entity.ProductsIncluded.Select(x => new ProductIncludedLookup(x)).ToList();
            foreach (var productDependentLookup in DependentProducts)
                await productDependentLookup.LoadOther(unitOfWork);
        }

        public List<ProductIncludedLookup> DependentProducts { get; private set; }
    }
}