using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ProducersSalesChart
{
    public class ProducersSalesChartItem : SalesChartItem
    {
        public ProducersSalesChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll) : base(salesUnits, sumOfAll)
        {
        }

        public override string ItemName => SalesUnits.First().Producer.ToString();

        public override string Title => "Производитель";
    }
}