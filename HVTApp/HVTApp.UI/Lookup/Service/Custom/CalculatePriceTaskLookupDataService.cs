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

            var blocks = await UnitOfWork.Repository<ProductBlock>().GetAllAsNoTrackingAsync();
            var salesUnits = await UnitOfWork.Repository<SalesUnit>().GetAllAsNoTrackingAsync();
            var offerUnits = await UnitOfWork.Repository<OfferUnit>().GetAllAsNoTrackingAsync();

            var pricelessBlocks = blocks.Where(x => !x.Prices.Any());
            var notActualBlocks = blocks.Where(x => x.Prices.Any() && x.Prices.Select(p => p.Date).Max().AddDays(CommonOptions.ActualPriceTerm) < DateTime.Today);
            foreach (var block in pricelessBlocks.Union(notActualBlocks))
            {
                var task = new CalculatePriceTask
                {
                    Status = block.Prices.Any() ? CalculatePriceTaskStatus.NotActual : CalculatePriceTaskStatus.IsEmpty,
                    ProductBlock = block
                };

                var projects = salesUnits.Where(x => x.Product.GetBlocks().Contains(block)).Select(x => x.Project).Distinct().ToList();
                var specs = salesUnits.Where(x => x.Product.GetBlocks().Contains(block)).Select(x => x.Specification).Distinct().ToList();
                var offers = offerUnits.Where(x => x.Product.GetBlocks().Contains(block)).Select(x => x.Offer).Distinct().ToList();

                //это надо доделать
                //if (task.Status == CalculatePriceTaskStatus.NotActual)
                //{
                //    specs = specs.Where(x => x.Date < DateTime.Today.AddDays(-CommonOptions.ActualPriceTerm)).ToList();
                //    offers = offers.Where(x => x.RegistrationDetailsOfSender != null && x.RegistrationDetailsOfSender.RegistrationDate < DateTime.Today.AddDays(-CommonOptions.ActualPriceTerm)).ToList();
                //}

                task.Date = DateTime.Today;
                task.Projects = projects;
                task.Specifications = specs;
                task.Offers = offers;
                
                result.Add(new CalculatePriceTaskLookup(task));
            }

            return result;
        }
    }
}