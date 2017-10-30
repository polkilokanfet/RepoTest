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
            root.Items.Add(new NavigationItem("Проекты", typeof(ProjectListView)));
            root.Items.Add(new NavigationItem("Конкурсы", typeof(TenderListView)));
            root.Items.Add(new NavigationItem("ТКП", typeof(OfferListView)));
            root.Items.Add(new NavigationItem("Контракты", typeof(ContractListView)));
            root.Items.Add(new NavigationItem("Платежи", typeof(PaymentListView)));

            Items.Add(root);
        }
    }
}
