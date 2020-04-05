using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart
{
    public class RegionsSalesChartViewModel : SalesChartViewModel<RegionsSalesChartItem>
    {
        public override string Title => "Продажи по регионам";

        public RegionsSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<RegionsSalesChartItem> GetItems()
        {
            return SalesUnitsFiltered
                .GroupBy(x => x.Facility.GetRegion())
                .Select(x => new RegionsSalesChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
