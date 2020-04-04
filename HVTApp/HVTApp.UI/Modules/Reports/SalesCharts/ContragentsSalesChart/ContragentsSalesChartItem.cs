using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart
{
    public class ContragentsSalesChartItem : SalesChartItem
    {
        public string Contragent => SalesUnits.First().Specification.Contract.Contragent.ToString();

        public ContragentsSalesChartItem(IEnumerable<SalesUnit> salesUnits) : base(salesUnits)
        {
        }
    }
}