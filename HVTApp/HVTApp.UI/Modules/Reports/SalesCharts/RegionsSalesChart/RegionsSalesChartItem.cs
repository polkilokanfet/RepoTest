using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart
{
    public class RegionsSalesChartItem : SalesChartItem
    {
        public string Region => SalesUnits.First().Facility.GetRegion()?.Name ?? "Не определен";

        public RegionsSalesChartItem(IEnumerable<SalesUnit> salesUnits) : base(salesUnits)
        {
        }
    }
}