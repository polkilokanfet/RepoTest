using HVTApp.Infrastructure;
using HVTApp.Modules.Products.Views;

namespace HVTApp.Modules.Products.Menus
{
    public class ProductsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            Items.Add(new NavigationItem("Параметры", typeof(ParametersView)));
            Items.Add(new NavigationItem("Задания", typeof(CreateNewProductTasksView)));
        }
    }
}
