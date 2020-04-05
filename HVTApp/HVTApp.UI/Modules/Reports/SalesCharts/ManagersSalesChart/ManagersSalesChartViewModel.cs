using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart
{
    public class ManagersSalesChartViewModel : SalesChartViewModel<ManagersSalesChartItem>
    {
        public override string Title => "Продажи по менеджерам";

        public ManagersSalesChartViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override List<SalesUnit> GetSalesUnits()
        {
            return UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon);
        }

        protected override List<ManagersSalesChartItem> GetItems()
        {
            return SalesUnitsFiltered
                .GroupBy(x => x.Project.Manager)
                .Select(x => new ManagersSalesChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();            
        }
    }
}
