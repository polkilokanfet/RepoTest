using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Director.Views;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.PriceCalculations.View;

namespace HVTApp.Modules.Director.Menus
{
    public class DirectorMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Рынок", typeof(MarketView)));
            Items.Add(new NavigationItem("Поступления (факт)", typeof(PaymentsActualView)));
            Items.Add(new NavigationItem("Поступления (план)", typeof(PaymentsPlanView)));
            Items.Add(new NavigationItem("Расчет ПЗ", typeof(PriceCalculationsView)));
        }
    }
}
