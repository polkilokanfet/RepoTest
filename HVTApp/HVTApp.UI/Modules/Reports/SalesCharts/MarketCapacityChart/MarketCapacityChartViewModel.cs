using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.MarketCapacityChart
{
    public class MarketCapacityChartViewModel : SalesChartViewModel<MarketCapacityChartItem>
    {
        public override string Title => "Ёмкость рынка";

        public MarketCapacityChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<SalesUnit> GetSalesUnits()
        {
            return base.GetSalesUnits().Where(x => x.Project.ForReport).ToList();
        }

        protected override List<MarketCapacityChartItem> GetItems()
        {
            return SalesUnitsFiltered
                .GroupBy(x => x.Product.ProductType)
                .Select(x => new MarketCapacityChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();            
        }
    }
}
