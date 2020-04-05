using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart
{
    public class ContragentsSalesChartItem : SalesChartItem
    {
        public ContragentsSalesChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll) : base(salesUnits, sumOfAll)
        {
        }

        public override string ItemName => SalesUnits.First().Specification.Contract.Contragent.ToString();

        public override string Title => "Контрагент";
    }
}