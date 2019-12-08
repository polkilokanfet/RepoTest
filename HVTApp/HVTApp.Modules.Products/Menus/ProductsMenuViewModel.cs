using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.Products.Views;

namespace HVTApp.Modules.Products.Menus
{
    public class ProductsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Параметры", typeof(ParametersView)));
                Items.Add(new NavigationItem("Задания", typeof(CreateNewProductTasksView)));
            }
            Items.Add(new NavigationItem("Стракчакосты", typeof(StructureCostsView)));
        }
    }
}
