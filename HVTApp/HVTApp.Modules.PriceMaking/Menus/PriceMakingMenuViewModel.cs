using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PriceMaking.Views;
using HVTApp.UI.PriceCalculations;
using PriceCalculationsView = HVTApp.UI.PriceCalculations.View.PriceCalculationsView;

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