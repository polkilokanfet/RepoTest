using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PriceCalculationItemRepository
    {
        protected override IQueryable<PriceCalculationItem> GetQuary()
        {
            return Context.Set<PriceCalculationItem>().AsQueryable()
                .Include(x => x.StructureCosts);
        }
    }
}