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
                .Include(block => block.Prices)
                .Include(block => block.FixedCosts)
                .Include(block => block.Parameters);
        }
    }
}