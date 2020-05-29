using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.PriorityReport
{
    public class PriorityReportGroup
    {
        public List<PriorityReportItem> Items { get; }

        public string ProductType => Items.First().Product.ProductType.Name;

        public int Amount => Items.Sum(x => x.Amount);

        public double Cost => Items.Sum(x => x.Cost);

        public double CostWithVat => Items.Sum(x => x.CostWithVat);

        public double SumPaid => Items.Sum(x => x.SumPaid);
        public double SumNotPaid => Items.Sum(x => x.SumNotPaid);
        public double SumPaidPercent => SumPaid / Cost * 100.0;


        public PriorityReportGroup(IEnumerable<SalesUnit> salesUnits)
        {
            Items = salesUnits
                .GroupBy(x => new
                {
                    x.Cost,
                    x.EndProductionDateCalculated,
                    x.Specification,
                    x.Order
                })
                .Select(x => new PriorityReportItem(x))
                .OrderBy(x => x.PickingDate)
                .ToList();
        }
    }
}