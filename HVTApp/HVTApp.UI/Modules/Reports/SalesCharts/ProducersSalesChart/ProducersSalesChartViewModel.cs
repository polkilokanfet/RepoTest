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
            return GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Producer != null && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Producer != null);
        }

        protected override List<ProducersSalesChartItem> GetItems()
        {
            var salesUnits = SalesUnits.Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate);
            if (Parameters.Any())
                salesUnits = salesUnits.Where(x => Parameters.AllContainsIn(x.Product.ProductBlock.Parameters));

            return salesUnits
                .GroupBy(x => x.Producer)
                .Select(x => new ProducersSalesChartItem(x, SumOfSalesUnits))
                .OrderByDescending(x => x.Sum)
                .ToList();            
        }
    }
}
