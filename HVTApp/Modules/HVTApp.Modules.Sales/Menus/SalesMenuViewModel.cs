using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.Views;

namespace HVTApp.Modules.Sales.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var root = new NavigationItem("Рынок", typeof(Market2View));
            root.Items.Add(new NavigationItem("Проекты", typeof(ProjectsView)));
            root.Items.Add(new NavigationItem("Предложения", typeof(OffersView)));
            root.Items.Add(new NavigationItem("Тендеры", typeof(TenderLookupListView)));
            root.Items.Add(new NavigationItem("Спецификации", typeof(SpecificationLookupListView)));
            root.Items.Add(new NavigationItem("Для оборудования", typeof(MarketView)));

            var payments = new NavigationItem("Поступления", typeof(PaymentsView));
            var production = new NavigationItem("Производство", typeof(ProductionView));
            var shipping = new NavigationItem("Отгрузка", typeof(ShippingView));

            Items.Add(root);
            Items.Add(production);
            Items.Add(payments);
            Items.Add(shipping);
        }
    }
}
