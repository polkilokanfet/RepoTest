using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookup
    {
        public OfferLookup(Offer offer, IEnumerable<OfferUnit> offerUnits) : base(offer)
        {
            OfferUnits.AddRange(offerUnits.Select(x => new OfferUnitLookup(x)));
        }
        public List<OfferUnitLookup> OfferUnits { get; } = new List<OfferUnitLookup>();

        [Designation("Компания"), OrderStatus(100)]
        public CompanyLookup Company => new CompanyLookup(Entity.RecipientEmployee.Company);

        [Designation("Сумма"), OrderStatus(90)]
        public double? Sum => OfferUnits?.Sum(x => x.Cost);
    }
}