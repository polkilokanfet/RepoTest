using System.Collections.ObjectModel;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Views;

namespace HVTApp.Modules.Sales.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem root = new NavigationItem("Market", typeof(ProjectsView));
            root.Items.Add(new NavigationItem("Projects", typeof(ProjectsView)));
            root.Items.Add(new NavigationItem("Payments", typeof(PaymentsView)));

            Items.Add(root);
        }
    }
}
