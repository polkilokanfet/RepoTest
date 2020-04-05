using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.MarketCapacityChart
{
    public class MarketCapacityChartItem : SalesChartItem
    {
        public MarketCapacityChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll) : base(salesUnits, sumOfAll)
        {
        }

        public override string ItemName => SalesUnits.First().Product.ProductType.ToString();

        public override string Title => "Тип оборудования";
    }
}