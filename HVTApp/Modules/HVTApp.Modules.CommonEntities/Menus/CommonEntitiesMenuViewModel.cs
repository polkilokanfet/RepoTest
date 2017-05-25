using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Views;

namespace HVTApp.Modules.CommonEntities.Menus
{
    public class CommonEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem rootCompany = new NavigationItem("Компании", typeof(CompaniesView));
            rootCompany.Items.Add(new NavigationItem("Организационные формы компаний", typeof(CompanyFormsView)));
            rootCompany.Items.Add(new NavigationItem("Сферы деятельности компаний", typeof(ActivityFildsView)));

            NavigationItem rootProduct = new NavigationItem("Products", typeof(ProductsView));

            Items.Add(rootCompany);
            Items.Add(rootProduct);
        }
    }
}
