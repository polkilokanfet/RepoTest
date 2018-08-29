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
            DependentProducts = Entity.DependentProducts.Select(x => new ProductDependentLookup(x)).ToList();
            foreach (var productDependentLookup in DependentProducts)
                await productDependentLookup.LoadOther(unitOfWork);
        }

        public List<ProductDependentLookup> DependentProducts { get; private set; }
    }
}