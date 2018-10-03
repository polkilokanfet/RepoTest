using HVTApp.Infrastructure;
using HVTApp.Modules.PlanAndEconomy.Views;

namespace HVTApp.Modules.PlanAndEconomy.Menus
{
    public class PlanAndEconomyMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var prices = new NavigationItem("Переменные затраты", typeof(PricesView));
            var prodPlan = new NavigationItem("План производства", typeof(ProductionPlanView));
            var dates = new NavigationItem("Фактические даты", typeof(DatesView));
            var paymentsF = new NavigationItem("Поступления (факт)", typeof(PaymentDocumentsView));
            var paymentsP = new NavigationItem("Поступления (план)", typeof(PaymentsView));


            Items.Add(prices);
            Items.Add(prodPlan);
            Items.Add(dates);
            Items.Add(paymentsF);
            Items.Add(paymentsP);
        }
    }
}
