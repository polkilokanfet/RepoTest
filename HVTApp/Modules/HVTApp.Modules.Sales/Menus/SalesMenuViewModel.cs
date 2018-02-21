using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.Views;

namespace HVTApp.Modules.Sales.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var root = new NavigationItem("Рынок", typeof(MarketView));
            root.Items.Add(new NavigationItem("Проекты", typeof(ProjectListView)));
            root.Items.Add(new NavigationItem("Позиции", typeof(SalesUnitListView)));
            root.Items.Add(new NavigationItem("Тендеры", typeof(TenderListView)));
            root.Items.Add(new NavigationItem("ТКП", typeof(OfferListView)));
            root.Items.Add(new NavigationItem("Контракты", typeof(ContractListView)));
            root.Items.Add(new NavigationItem("Плановые платежи", typeof(PaymentPlannedListView)));
            root.Items.Add(new NavigationItem("Плановые платежи 2", typeof(PaymentPlannedListGeneratorView)));
            root.Items.Add(new NavigationItem("OIT", typeof(OitView)));

            Items.Add(root);
        }
    }
}
