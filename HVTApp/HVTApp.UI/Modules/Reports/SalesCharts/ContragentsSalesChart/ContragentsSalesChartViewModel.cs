using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart
{
    public class ContragentsSalesChartViewModel : SalesChartViewModel<ContragentsSalesChartItem>
    {
        public ContragentsSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<SalesUnit> GetSalesUnits()
        {
            return base.GetSalesUnits().Where(x => x.Specification != null).ToList();
        }

        protected override List<ContragentsSalesChartItem> GetItems()
        {
            return SalesUnits
                .Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate)
                .GroupBy(x => x.Facility.OwnerCompany)
                .Select(x => new ContragentsSalesChartItem(x))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
