using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart
{
    public class ProductTypesSalesChartViewModel : SalesChartViewModel<ProductTypesSalesChartItem>
    {
        public override string Title => "������� �� ����� ������������";

        public ProductTypesSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<ProductTypesSalesChartItem> GetItems()
        {
            return SalesUnitsFiltered
                .GroupBy(x => x.Product.ProductType)
                .Select(x => new ProductTypesSalesChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}
