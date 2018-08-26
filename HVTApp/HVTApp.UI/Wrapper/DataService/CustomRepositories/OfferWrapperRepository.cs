using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapperRepository
    {
        protected override async Task<OfferWrapper> GenerateWrapper(Offer offer)
        {
            var offerUnits = await UnitOfWorkWrapper.GetWrapperRepository<OfferUnit, OfferUnitWrapper>().FindAsync(x => Equals(x.Offer.Model, offer));
            return new OfferWrapper(offer, offerUnits);
        }
    }
}