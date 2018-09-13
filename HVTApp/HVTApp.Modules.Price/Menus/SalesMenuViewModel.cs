using HVTApp.Infrastructure;
using HVTApp.Modules.Price.Views;

namespace HVTApp.Modules.Price.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var tsks = new NavigationItem("Задачи", typeof(PricesView));
            var prodPlan = new NavigationItem("План производства", typeof(ProductionPlanView));
            var dates = new NavigationItem("Фактические даты", typeof(DatesView));
            var payments = new NavigationItem("Поступления (факт)", typeof(PaymentDocumentsView));
            
            Items.Add(tsks);
            Items.Add(prodPlan);
            Items.Add(dates);
            Items.Add(payments);
        }
    }
}
