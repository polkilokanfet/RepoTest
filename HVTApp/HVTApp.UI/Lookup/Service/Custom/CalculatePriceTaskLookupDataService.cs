using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CalculatePriceTaskLookupDataService
    {
        public override async Task<IEnumerable<CalculatePriceTaskLookup>> GetAllLookupsAsync()
        {
            var result = new List<CalculatePriceTaskLookup>();

            var blocks = await UnitOfWork.GetRepository<ProductBlock>().GetAllAsNoTrackingAsync();
            var salesUnits = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsNoTrackingAsync();
            var offerUnits = await UnitOfWork.GetRepository<OfferUnit>().GetAllAsNoTrackingAsync();

            var pricelessBlocks = blocks.Where(x => !x.Prices.Any());
            var notActualBlocks = blocks.Where(x => x.Prices.Any() && x.Prices.Select(p => p.Date).Max().AddDays(CommonOptions.ActualPriceTerm) < DateTime.Today);
            foreach (var block in pricelessBlocks.Union(notActualBlocks))
            {
                var projects = salesUnits.Where(x => x.Product.GetBlocks().Contains(block)).Select(x => x.Project).Distinct().ToList();
                var specs = salesUnits.Where(x => x.Product.GetBlocks().Contains(block)).Select(x => x.Specification).Distinct().ToList();
                var offers = offerUnits.Where(x => x.Product.GetBlocks().Contains(block)).Select(x => x.Offer).Distinct().ToList();

                var task = new CalculatePriceTask
                {
                    Status = CalculatePriceTaskStatus.IsEmpty,
                    ProductBlock = block,
                    Date = DateTime.Today,
                    Projects = projects,
                    Specifications = specs,
                    Offers = offers
                };
                task.Status = block.Prices.Any() ? CalculatePriceTaskStatus.NotActual : CalculatePriceTaskStatus.IsEmpty;
                result.Add(new CalculatePriceTaskLookup(task));
            }

            return result;
        }
    }
}