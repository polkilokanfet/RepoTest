using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Views;

namespace HVTApp.Modules.Sales.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var market = new NavigationItem("Рынок", typeof(Market2View));
            //market.Items.Add(new NavigationItem("Проекты", typeof(ProjectsView)));
            market.Items.Add(new NavigationItem("Предложения", typeof(OffersView)));
            market.Items.Add(new NavigationItem("Спецификации", typeof(SpecificationsView)));
            market.IsExpended = false;

            var priceCalculations = new NavigationItem("Расчеты стоимости", typeof(PriceCalculationsView));
            var payments = new NavigationItem("Поступления", typeof(PaymentsView));
            var production = new NavigationItem("Производство", typeof(ProductionView));
            var shipping = new NavigationItem("Отгрузка", typeof(ShippingView));

            Items.Add(market);
            Items.Add(priceCalculations);
            Items.Add(production);
            Items.Add(payments);
            Items.Add(shipping);
        }
    }
}
