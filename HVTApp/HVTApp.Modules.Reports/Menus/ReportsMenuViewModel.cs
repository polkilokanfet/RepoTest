using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.Reports.CommonInfo;
using HVTApp.UI.Modules.Reports.FlatReport;
using HVTApp.UI.Modules.Reports.MarketReport;
using HVTApp.UI.Modules.Reports.PriorityReport;
using HVTApp.UI.Modules.Reports.Reference;
using HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.MarketCapacityChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ProducersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart;
using HVTApp.UI.Modules.Reports.Views;

namespace HVTApp.Modules.Reports.Menus
{
    public class ReportsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                GlobalAppProperties.User.RoleCurrent == Role.SalesManager ||
                GlobalAppProperties.User.RoleCurrent == Role.Director)
                Items.Add(new NavigationItem("Рынок", typeof(MarketReportView)));

            Items.Add(new NavigationItem("Референс", typeof(ReferenceView)));
            Items.Add(new NavigationItem("Сводная информация (для счета)", typeof(CommonInfoView)));
            Items.Add(new NavigationItem("Report maker", typeof(FlatReportView)));
            //Items.Add(new NavigationItem("Продажи", typeof(SalesReportView)));

            if (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                GlobalAppProperties.User.RoleCurrent == Role.ReportMaker ||
                GlobalAppProperties.User.RoleCurrent == Role.Economist ||
                GlobalAppProperties.User.RoleCurrent == Role.Director)
                Items.Add(new NavigationItem("Очередность", typeof(PriorityReportView)));

            Items.Add(new NavigationItem("График продаж", typeof(SalesChartView)));

            var item = new NavigationItem("Аналитика", typeof(ManagersSalesChartView));
            item.Items.Add(new NavigationItem("Ёмкость рынка", typeof(MarketCapacityChartView)));
            item.Items.Add(new NavigationItem("Продажи по менеджерам", typeof(ManagersSalesChartView)));
            item.Items.Add(new NavigationItem("Продажи по типам оборудования", typeof(ProductTypesSalesChartView)));
            item.Items.Add(new NavigationItem("Продажи по регионам", typeof(RegionsSalesChartView)));
            item.Items.Add(new NavigationItem("Продажи по потребителям", typeof(ConsumersSalesChartView)));
            item.Items.Add(new NavigationItem("Продажи по контрагентам", typeof(ContragentsSalesChartView)));
            item.Items.Add(new NavigationItem("Продажи по производителям", typeof(ProducersSalesChartView)));
            Items.Add(item);
        }
    }
}
