using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart
{
    public class ProductTypesSalesChartViewModel : SalesChartViewModel<ProductTypesSalesChartItem>
    {
        public ProductTypesSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<ProductTypesSalesChartItem> GetItems()
        {
            return SalesUnits
                .Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate)
                .GroupBy(x => x.Product.ProductType)
                .Select(x => new ProductTypesSalesChartItem(x))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
