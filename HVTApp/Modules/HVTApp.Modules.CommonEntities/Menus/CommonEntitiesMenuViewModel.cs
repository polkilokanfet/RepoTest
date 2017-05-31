using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Views;

namespace HVTApp.Modules.CommonEntities.Menus
{
    public class CommonEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem rootFacility = new NavigationItem("Объекты", typeof(FacilitiesView));
            rootFacility.Items.Add(new NavigationItem("Тип объекта", typeof(FacilityTypesView)));

            NavigationItem rootCompany = new NavigationItem("Компании", typeof(CompaniesView));
            rootCompany.Items.Add(new NavigationItem("Организационные формы", typeof(CompanyFormsView)));
            rootCompany.Items.Add(new NavigationItem("Сферы деятельности", typeof(ActivityFildsView)));

            NavigationItem rootParameter = new NavigationItem("Параметры", typeof(ParametersView));
            rootParameter.Items.Add(new NavigationItem("Группа параметров", typeof(ParametersGroupsView)));

            NavigationItem rootProduct = new NavigationItem("Изделия", typeof(ProductsView));

            Items.Add(rootFacility);
            Items.Add(rootCompany);
            Items.Add(rootParameter);
            Items.Add(rootProduct);
        }
    }
}
