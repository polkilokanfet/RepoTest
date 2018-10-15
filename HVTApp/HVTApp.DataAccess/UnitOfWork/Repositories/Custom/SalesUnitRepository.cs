using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitRepository
    {
        protected override IQueryable<SalesUnit> GetQuary()
        {
            return Context.Set<SalesUnit>().AsQueryable()
                .Include(x => x.Facility)
                .Include(x => x.Project.Manager)
                .Include(x => x.Product.ProductBlock.Parameters)
                .Include(x => x.Product.DependentProducts.Select(dp => dp.Product.ProductBlock.Parameters))
                .Include(x => x.ProductsIncluded.Select(dp => dp.Product.ProductBlock.Parameters))
                .Include(x => x.PaymentsActual)
                .Include(x => x.PaymentsPlanned)
                .Include(x => x.Order);
        }
    }
}