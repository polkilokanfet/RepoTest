using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.Views;

namespace HVTApp.Modules.Sales.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem root = new NavigationItem("Рынок", typeof(MarketView));
            root.Items.Add(new NavigationItem("Проекты", typeof(ProjectsView)));
            root.Items.Add(new NavigationItem("Конкурсы", typeof(TendersView)));
            root.Items.Add(new NavigationItem("ТКП", typeof(OffersView)));
            root.Items.Add(new NavigationItem("Контракты", typeof(ContractsView)));
            root.Items.Add(new NavigationItem("Платежи", typeof(PaymentsView)));

            Items.Add(root);
        }
    }
}
