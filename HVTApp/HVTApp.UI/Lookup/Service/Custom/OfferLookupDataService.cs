using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookupDataService
    {
        public override async Task<IEnumerable<OfferLookup>> GetAllLookupsAsync()
        {
            var offers = await UnitOfWork.GetRepository<Offer>().GetAllAsNoTrackingAsync();
            var offerUnits = await UnitOfWork.GetRepository<OfferUnit>().GetAllAsNoTrackingAsync();

            return offers.Select(x => new OfferLookup(x, offerUnits.Where(ou => Equals(ou.Offer, x))));
        }
    }
}