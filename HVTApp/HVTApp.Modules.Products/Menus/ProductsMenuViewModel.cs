using HVTApp.Infrastructure;
using HVTApp.Modules.Products.Views;

namespace HVTApp.Modules.Products.Menus
{
    public class ProductsMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var root = new NavigationItem("Параметры", typeof(ParametersView));
            Items.Add(root);
        }
    }
}
