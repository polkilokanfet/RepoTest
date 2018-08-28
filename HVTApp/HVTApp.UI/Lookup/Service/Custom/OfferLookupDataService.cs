using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookupDataService
    {
        //public override async Task<IEnumerable<OfferLookup>> GetAllLookupsAsync()
        //{
        //    var offers = await UnitOfWork.GetRepository<Offer>().GetAllAsNoTrackingAsync();
        //    var offerUnits = await UnitOfWork.GetRepository<OfferUnit>().GetAllAsNoTrackingAsync();

        //    return offers.Select(x => new OfferLookup(x, offerUnits.Where(ou => Equals(ou.Offer, x))));
        //}

        //public override async Task<OfferLookup> GetLookupById(Guid id)
        //{
        //    var offer = await UnitOfWork.GetRepository<Offer>().GetByIdAsync(id);
        //    if (offer == null) return null;
        //    var offerUnits = UnitOfWork.GetRepository<OfferUnit>().Find(x => Equals(offer, x.Offer));
        //    return new OfferLookup(offer, offerUnits);
        //}
    }
}