using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Director.Views;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using PaymentsActualView = HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual.PaymentsActualView;
using PaymentsPlanView = HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan.PaymentsPlanView;

namespace HVTApp.Modules.Director.Menus
{
    public class DirectorMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Рынок", typeof(MarketView)));
            Items.Add(new NavigationItem("Поступления (факт)", typeof(PaymentsActualView)));
            Items.Add(new NavigationItem("Поступления (план)", typeof(PaymentsPlanView)));
        }
    }
}
