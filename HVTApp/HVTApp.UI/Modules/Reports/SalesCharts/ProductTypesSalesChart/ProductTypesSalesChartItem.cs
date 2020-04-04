using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart
{
    public class ProductTypesSalesChartItem : SalesChartItem
    {
        public string ProductType => SalesUnits.First().Product.ProductType.ToString();

        public ProductTypesSalesChartItem(IEnumerable<SalesUnit> salesUnits) : base(salesUnits)
        {
        }
    }
}