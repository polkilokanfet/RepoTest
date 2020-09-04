using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductBlockRepository
    {
        protected override IQueryable<ProductBlock> GetQuary()
        {
            return Context.Set<ProductBlock>().AsQueryable()
                .Include(x => x.Prices)
                .Include(x => x.FixedCosts)
                .Include(x => x.Parameters);
        }
    }
}