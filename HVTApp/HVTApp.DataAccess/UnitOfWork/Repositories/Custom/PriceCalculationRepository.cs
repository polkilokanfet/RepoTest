using System.Collections.Generic;
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
                .Include(calculation => calculation.PriceCalculationItems)
                .Include(calculation => calculation.PriceCalculationItems.Select(item => item.StructureCosts))
                .Include(calculation => calculation.PriceCalculationItems.Select(item => item.SalesUnits));
        }

        public List<PriceCalculationItem> GetCalculationsForPriceService(User user = null)
        {
            var queryable = Context.Set<PriceCalculation>().AsQueryable()
                .Include(priceCalculation => priceCalculation.PriceCalculationItems)
                .Where(priceCalculation => priceCalculation.PriceCalculationItems.Any())
                .Include(priceCalculation => priceCalculation.PriceCalculationItems.Select(item => item.StructureCosts));

            if (user == null)
            {
                queryable = queryable
                    .Include(priceCalculation => priceCalculation.PriceCalculationItems.Select(item => item.SalesUnits));
            }
            else
            {
                queryable = queryable
                    .Include(priceCalculation => priceCalculation.PriceCalculationItems.Select(item => item.SalesUnits.Select(salesUnit => salesUnit.Project.Manager)))
                    .Where(calculation => calculation.PriceCalculationItems.Any(item => item.SalesUnits.Any() && item.SalesUnits.Any(salesUnit => salesUnit.Project.Manager.Id == user.Id)));
            }


            return queryable
                .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                .Where(item => item.SalesUnits.Any())
                .ToList();
        }
    }
}