using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ProducersSalesChart
{
    public class ProducersSalesChartViewModel : SalesChartViewModel<ProducersSalesChartItem>
    {
        public override string Title => "Продажи по производителям";

        public ProducersSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<SalesUnit> GetSalesUnits()
        {
            return base.GetSalesUnits().Where(x => x.Producer != null).ToList();
        }

        protected override List<ProducersSalesChartItem> GetItems()
        {
            return SalesUnitsFiltered
                .GroupBy(x => x.Producer)
                .Select(x => new ProducersSalesChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();            
        }
    }
}
