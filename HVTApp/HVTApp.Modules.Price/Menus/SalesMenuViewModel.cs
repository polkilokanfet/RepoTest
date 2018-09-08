using HVTApp.Infrastructure;
using HVTApp.Modules.Price.Views;

namespace HVTApp.Modules.Price.Menus
{
    public class SalesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var root = new NavigationItem("Задачи", typeof(PricesView));
            Items.Add(root);
        }
    }
}
