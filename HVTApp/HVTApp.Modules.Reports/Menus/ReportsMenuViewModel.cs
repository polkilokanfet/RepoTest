using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Reports.Views;

namespace HVTApp.Modules.Reports.Menus
{
    public class ReportsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Референс", typeof(ReferenceView)));
            Items.Add(new NavigationItem("Продажи", typeof(SalesReportView)));
        }
    }
}
