using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart
{
    public class ContragentsSalesChartViewModel : SalesChartViewModel<ContragentsSalesChartItem>
    {
        public override string Title => "Продажи по контрагентам";

        public ContragentsSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<SalesUnit> GetSalesUnits()
        {
            return base.GetSalesUnits().Where(x => x.Specification != null).ToList();
        }

        protected override List<ContragentsSalesChartItem> GetItems()
        {
            return SalesUnitsFiltered
                .GroupBy(x => x.Facility.OwnerCompany)
                .Select(x => new ContragentsSalesChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
