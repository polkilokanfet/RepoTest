using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.PriceCalculations;
using Market2View = HVTApp.UI.Modules.Sales.Views.MarketView.Market2View;
using PaymentsView = HVTApp.UI.Modules.Sales.Views.PaymentsView;
using PriceCalculationsView = HVTApp.UI.PriceCalculations.View.PriceCalculationsView;
using ProductionView = HVTApp.UI.Modules.Sales.Views.ProductionView;
using ShippingView = HVTApp.UI.Modules.Sales.Views.ShippingView;

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
            var paymentsActual = new NavigationItem("Поступления (факт)", typeof(PaymentsActualView));
            var paymentsPlan = new NavigationItem("Поступления (план)", typeof(PaymentsView));
            var production = new NavigationItem("Производство", typeof(ProductionView));
            var shipping = new NavigationItem("Отгрузка", typeof(ShippingView));

            Items.Add(market);
            Items.Add(priceCalculations);
            Items.Add(production);
            Items.Add(paymentsActual);
            Items.Add(paymentsPlan);
            Items.Add(shipping);
        }
    }
}
