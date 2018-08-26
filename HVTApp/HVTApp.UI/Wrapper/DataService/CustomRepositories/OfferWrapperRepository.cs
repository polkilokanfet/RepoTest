using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapperRepository
    {
        protected override async Task<OfferWrapper> GenerateWrapper(Offer offer)
        {
            var offerUnits = await UnitOfWork.GetRepository<OfferUnit>().FindAsync(x => Equals(x.Offer, offer));
            return new OfferWrapper(offer, offerUnits.Select(x => new OfferUnitWrapper(x)));
        }
    }
}