using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferUnitRepository
    {
        protected override IQueryable<OfferUnit> GetQuary()
        {
            return Context.Set<OfferUnit>().AsQueryable()
                .Include(offerUnit => offerUnit.Facility)
                .Include(offerUnit => offerUnit.Offer.Project.Manager)
                .Include(offerUnit => offerUnit.Product.ProductBlock.Parameters)
                .Include(offerUnit => offerUnit.ProductsIncluded.Select(pi => pi.Product.ProductBlock));
        }

        public IEnumerable<OfferUnit> GetAllOfCurrentUser()
        {
            return this.GetQuary()
                .Where(offerUnit => offerUnit.Offer.Project.Manager.Id == GlobalAppProperties.User.Id)
                .ToList();
        }

        public IEnumerable<OfferUnit> GetByOffer(Offer offer)
        {
            return this.GetQuary()
                .Include(offerUnit => offerUnit.PaymentConditionSet)
                .Include(offerUnit => offerUnit.ProductsIncluded.Select(productIncluded => productIncluded.Product))
                .Where(offerUnit => offerUnit.Offer.Id == offer.Id)
                .ToList();
        }
    }
}