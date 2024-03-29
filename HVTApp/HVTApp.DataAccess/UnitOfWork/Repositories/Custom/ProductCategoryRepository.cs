using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductCategoryRepository
    {
        protected override IQueryable<ProductCategory> GetQuery()
        {
            return Context.Set<ProductCategory>().AsQueryable()
                .Include(x => x.Parameters);
        }
    }
}