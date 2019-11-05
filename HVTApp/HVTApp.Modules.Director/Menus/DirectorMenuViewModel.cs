using HVTApp.Infrastructure;
using HVTApp.Modules.Director.Views;

namespace HVTApp.Modules.Director.Menus
{
    public class DirectorMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var salesReport = new NavigationItem("Рынок", typeof(MarketView));
            Items.Add(salesReport);
        }
    }
}
