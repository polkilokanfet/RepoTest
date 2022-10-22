using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PriceCalculationRepository
    {
        protected override IQueryable<PriceCalculation> GetQuery()
        {
            return Context.Set<PriceCalculation>().AsQueryable()
                .Include(x => x.PriceCalculationItems)
                .Include(x => x.PriceCalculationItems.Select(item => item.StructureCosts))
                .Include(x => x.PriceCalculationItems.Select(item => item.SalesUnits));
        }
    }
}