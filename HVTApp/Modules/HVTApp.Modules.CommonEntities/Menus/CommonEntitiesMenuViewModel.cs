using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Views;

namespace HVTApp.Modules.CommonEntities.Menus
{
    public class CommonEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem rootCompany = new NavigationItem("Companies", typeof(CompaniesView));
            rootCompany.Items.Add(new NavigationItem("CompanyForms", typeof(CompanyFormsView)));

            NavigationItem rootProduct = new NavigationItem("Products", typeof(ProductsView));

            Items.Add(rootCompany);
            Items.Add(rootProduct);
        }
    }
}
