using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Director.Views;
using HVTApp.UI.Modules.PlanAndEconomy.Views;

namespace HVTApp.Modules.Director.Menus
{
    public class DirectorMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Рынок", typeof(MarketView)));
            Items.Add(new NavigationItem("Поступления (факт)", typeof(PaymentsActualView)));
        }
    }
}
