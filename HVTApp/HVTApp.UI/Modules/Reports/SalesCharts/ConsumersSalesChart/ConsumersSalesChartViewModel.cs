using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart
{
    public class ConsumersSalesChartViewModel : SalesChartViewModel<ConsumersSalesChartItem>
    {
        public override string Title => "Продажи по потребителям";

        public ConsumersSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<ConsumersSalesChartItem> GetItems()
        {
            return SalesUnitsFiltered
                .GroupBy(x => x.Facility.OwnerCompany)
                .Select(x => new ConsumersSalesChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
