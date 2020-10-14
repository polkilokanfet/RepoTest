using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Modules.Sales.Production;
using HVTApp.UI.Modules.Sales.Shippings;
using HVTApp.UI.Modules.Sales.Supervision;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.TechnicalRequrementsTasksModule;

namespace HVTApp.Modules.Sales.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var market = new NavigationItem("Рынок", typeof(Market2View));
            market.Items.Add(new NavigationItem("Предложения", typeof(OffersView)));
            market.Items.Add(new NavigationItem("Спецификации", typeof(SpecificationsView)));
            market.IsExpended = true;

            var reqs = new NavigationItem("Задачи в ТСЕ", typeof(TechnicalRequrementsTasksView));
            var priceCalculations = new NavigationItem("Расчеты стоимости", typeof(PriceCalculationsView));
            var paymentsActual = new NavigationItem("Поступления (факт)", typeof(PaymentsActualView));
            var paymentsPlan = new NavigationItem("Поступления (план)", typeof(UI.Modules.Sales.Payments.PaymentsView));
            var production = new NavigationItem("Производство", typeof(ProductionView));
            var shipping = new NavigationItem("Отгрузка", typeof(ShippingView));
            var supervision = new NavigationItem("Шеф-монтаж", typeof(SupervisionView));

            Items.Add(market);
            Items.Add(reqs);
            Items.Add(priceCalculations);
            Items.Add(production);
            Items.Add(paymentsActual);
            Items.Add(paymentsPlan);
            Items.Add(shipping);
            Items.Add(supervision);
        }
    }
}
