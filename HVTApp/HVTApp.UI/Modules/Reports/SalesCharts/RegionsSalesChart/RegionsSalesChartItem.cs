using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart
{
    public class RegionsSalesChartItem : SalesChartItem
    {
        public RegionsSalesChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll) : base(salesUnits, sumOfAll)
        {
        }

        public override string ItemName => SalesUnits.First().Facility.GetRegion()?.Name ?? "Не определен";

        public override string Title => "Регион";
    }
}