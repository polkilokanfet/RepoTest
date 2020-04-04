using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart
{
    public class ConsumersSalesChartViewModel : SalesChartViewModel<ConsumersSalesChartItem>
    {
        public ConsumersSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<ConsumersSalesChartItem> GetItems()
        {
            return SalesUnits
                .Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate)
                .GroupBy(x => x.Facility.OwnerCompany)
                .Select(x => new ConsumersSalesChartItem(x))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
