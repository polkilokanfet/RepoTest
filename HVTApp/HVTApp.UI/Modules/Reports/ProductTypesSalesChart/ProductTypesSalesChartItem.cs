using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.ProductTypesSalesChart
{
    public class ProductTypesSalesChartItem
    {
        private readonly List<SalesUnit> _salesUnits;

        public string ProductType => _salesUnits.First().Product.ProductType.ToString();

        public double Sum => _salesUnits.Sum(x => x.Cost);

        public ProductTypesSalesChartItem(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();
        }
    }
}