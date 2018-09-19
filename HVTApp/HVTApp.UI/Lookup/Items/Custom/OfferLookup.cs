using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookup
    {
        public List<OfferUnitLookup> OfferUnits { get; private set; }

        [Designation("Компания")]
        public CompanyLookup Company => new CompanyLookup(Entity.RecipientEmployee.Company);

        public double? Sum => OfferUnits?.Sum(x => x.Cost);
    }
}