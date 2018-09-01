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
            DependentProducts = Entity.DependentProducts.Select(x => new ProductAdditionalLookup(x)).ToList();
            foreach (var productDependentLookup in DependentProducts)
                await productDependentLookup.LoadOther(unitOfWork);
        }

        public List<ProductAdditionalLookup> DependentProducts { get; private set; }
    }
}