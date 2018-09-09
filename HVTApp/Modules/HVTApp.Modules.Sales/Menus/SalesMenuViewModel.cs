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
            root.Items.Add(new NavigationItem("Для оборудования", typeof(MarketView)));
            root.Items.Add(new NavigationItem("Проекты", typeof(ProjectLookupListView)));
            root.Items.Add(new NavigationItem("ТКП", typeof(OffersView)));
            root.Items.Add(new NavigationItem("Поступления", typeof(PaymentsView)));
            root.Items.Add(new NavigationItem("Позиции", typeof(SalesUnitLookupListView)));
            root.Items.Add(new NavigationItem("Тендеры", typeof(TenderLookupListView)));
            root.Items.Add(new NavigationItem("ТКП", typeof(OfferLookupListView)));
            root.Items.Add(new NavigationItem("Контракты", typeof(ContractLookupListView)));
            root.Items.Add(new NavigationItem("Спецификации", typeof(SpecificationLookupListView)));

            var production = new NavigationItem("Производство", typeof(ProductionView));

            Items.Add(root);
            Items.Add(production);
        }
    }
}
