using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using HVTApp.UI.PriceCalculations;
using PricesView = HVTApp.UI.Modules.PriceMaking.Views.PricesView;

namespace HVTApp.Modules.PriceMaking.Menus
{
    public class PriceMakingMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Переменные затраты", typeof(PricesView)));
            Items.Add(new NavigationItem("Расчет себестоимости", typeof(PriceCalculationsView)));
        }
    }
}