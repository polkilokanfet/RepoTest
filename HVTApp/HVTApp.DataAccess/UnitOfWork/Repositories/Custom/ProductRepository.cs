using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductRepository
    {
        protected override IQueryable<Product> GetQuary()
        {
            return Context.Set<Product>().AsQueryable()
                .Include(x => x.ProductBlock)
                .Include(x => x.DependentProducts);
        }
    }
}
