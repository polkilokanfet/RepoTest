using HVTApp.Infrastructure;
using HVTApp.Modules.Reports.Views;

namespace HVTApp.Modules.Reports.Menus
{
    public class ReportsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var salesReport = new NavigationItem("Продажи", typeof(SalesReportView));
            Items.Add(salesReport);
        }
    }
}
