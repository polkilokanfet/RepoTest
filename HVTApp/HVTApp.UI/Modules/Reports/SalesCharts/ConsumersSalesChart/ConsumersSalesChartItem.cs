using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart
{
    public class ConsumersSalesChartItem : SalesChartItem
    {
        public string Consumer => SalesUnits.First().Facility.OwnerCompany.ToString();

        public ConsumersSalesChartItem(IEnumerable<SalesUnit> salesUnits) : base(salesUnits)
        {
        }
    }
}