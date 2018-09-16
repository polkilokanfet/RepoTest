using HVTApp.Infrastructure;
using HVTApp.Modules.Price.Views;

namespace HVTApp.Modules.Price.Menus
{
    public class PriceMenuViewModel : BaseMenuViewModel
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
