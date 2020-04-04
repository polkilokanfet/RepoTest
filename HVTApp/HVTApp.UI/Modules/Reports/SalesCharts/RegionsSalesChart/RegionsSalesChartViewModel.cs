using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart
{
    public class RegionsSalesChartViewModel : SalesChartViewModel<RegionsSalesChartItem>
    {
        public RegionsSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<RegionsSalesChartItem> GetItems()
        {
            return SalesUnits
                .Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate)
                .GroupBy(x => x.Facility.GetRegion())
                .Select(x => new RegionsSalesChartItem(x))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
