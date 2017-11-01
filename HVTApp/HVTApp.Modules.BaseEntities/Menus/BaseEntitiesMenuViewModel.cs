using HVTApp.Infrastructure;
using HVTApp.UI.Views;

namespace HVTApp.Modules.BaseEntities.Menus
{
    public class BaseEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem rootFacility = new NavigationItem("Объекты", typeof(FacilityListView));
            rootFacility.Items.Add(new NavigationItem("Тип объекта", typeof(FacilityTypeListView)));

            NavigationItem rootCompany = new NavigationItem("Компании", typeof(CompanyListView));
            rootCompany.Items.Add(new NavigationItem("Организационные формы", typeof(CompanyFormListView)));
            rootCompany.Items.Add(new NavigationItem("Сферы деятельности", typeof(ActivityFieldListView)));

            NavigationItem rootParameter = new NavigationItem("Параметры", typeof(ParameterListView));
            rootParameter.Items.Add(new NavigationItem("Группа параметров", typeof(ParameterGroupListView)));

            NavigationItem rootProduct = new NavigationItem("Изделия", typeof(ProductListView));

            Items.Add(rootFacility);
            Items.Add(rootCompany);
            Items.Add(rootParameter);
            Items.Add(rootProduct);
        }
    }
}
