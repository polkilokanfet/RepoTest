using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Reports.FlatReport;
using HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart;
using HVTApp.UI.Modules.Reports.Views;

namespace HVTApp.Modules.Reports.Menus
{
    public class ReportsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Референс", typeof(ReferenceView)));
            Items.Add(new NavigationItem("Report maker", typeof(FlatReportView)));
            Items.Add(new NavigationItem("Продажи", typeof(SalesReportView)));
            Items.Add(new NavigationItem("График продаж", typeof(SalesChartView)));
            Items.Add(new NavigationItem("Продажи по менеджерам", typeof(ManagersSalesChartView)));
            Items.Add(new NavigationItem("Продажи по типам оборудования", typeof(ProductTypesSalesChartView)));
            Items.Add(new NavigationItem("Продажи по регионам", typeof(RegionsSalesChartView)));
            Items.Add(new NavigationItem("Продажи по потребителям", typeof(ConsumersSalesChartView)));
            Items.Add(new NavigationItem("Продажи по контрагентам", typeof(ContragentsSalesChartView)));
        }
    }
}
