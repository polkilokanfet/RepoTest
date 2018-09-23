using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookup
    {
        public List<OfferUnitLookup> OfferUnits { get; } = new List<OfferUnitLookup>();

        [Designation("Компания"), OrderStatus(20)]
        public CompanyLookup Company => new CompanyLookup(Entity.RecipientEmployee.Company);

        [Designation("Сумма"), OrderStatus(19)]
        public double? Sum => OfferUnits?.Sum(x => x.Cost);
    }
}