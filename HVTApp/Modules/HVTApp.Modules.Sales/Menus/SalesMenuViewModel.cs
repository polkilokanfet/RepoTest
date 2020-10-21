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
            market.Items.Add(new NavigationItem("Задачи в ТСЕ", typeof(TechnicalRequrementsTasksView)));
            market.Items.Add(new NavigationItem("Расчеты переменных затрат", typeof(PriceCalculationsView)));
            market.Items.Add(new NavigationItem("Предложения", typeof(OffersView)));
            market.Items.Add(new NavigationItem("Спецификации", typeof(SpecificationsView)));
            market.IsExpended = true;

            var payments = new NavigationItem("Поступления", typeof(PaymentsActualView));
            payments.Items.Add(new NavigationItem("Планируемые", typeof(UI.Modules.Sales.Payments.PaymentsView)));
            payments.Items.Add(new NavigationItem("Фактические", typeof(PaymentsActualView)));
            payments.IsExpended = true;

            var production = new NavigationItem("Исполнение", typeof(ProductionView));
            production.Items.Add(new NavigationItem("Производство", typeof(ProductionView)));
            production.Items.Add(new NavigationItem("Отгрузка", typeof(ShippingView)));
            production.Items.Add(new NavigationItem("Шеф-монтаж", typeof(SupervisionView)));
            production.IsExpended = true;

            Items.Add(market);
            Items.Add(payments);
            Items.Add(production);
        }
    }
}
