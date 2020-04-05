using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart
{
    public class ConsumersSalesChartItem : SalesChartItem
    {
        public override string ItemName => SalesUnits.First().Facility.OwnerCompany.ToString();

        public override string Title => "Потребитель";

        public ConsumersSalesChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll) : base(salesUnits, sumOfAll)
        {
        }
    }
}