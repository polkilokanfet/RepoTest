using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookup
    {
        public OfferLookup(Offer offer, IEnumerable<OfferUnit> offerUnits) : base(offer)
        {
            OfferUnits = offerUnits.Select(x => new OfferUnitLookup(x)).ToList();
        }

        public CompanyLookup Company => new CompanyLookup(Entity.RecipientEmployee.Company);

        public List<OfferUnitLookup> OfferUnits { get; set; }
    }
}