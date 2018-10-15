using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferUnitRepository
    {
        protected override IQueryable<OfferUnit> GetQuary()
        {
            return Context.Set<OfferUnit>().AsQueryable()
                .Include(x => x.Facility)
                .Include(x => x.Product.ProductBlock.Parameters)
                .Include(x => x.ProductsIncluded.Select(pi => pi.Product.ProductBlock));
        }
    }
}