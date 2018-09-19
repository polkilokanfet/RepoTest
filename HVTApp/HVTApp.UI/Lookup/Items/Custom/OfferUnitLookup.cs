using System.Collections.Generic;

namespace HVTApp.UI.Lookup
{
    public partial class OfferUnitLookup : IUnitLookup
    {
        public List<ProductIncludedLookup> DependentProducts { get; private set; }
    }
}