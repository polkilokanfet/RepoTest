using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.UI.Views;

namespace HVTApp.Modules.BaseEntities.Menus
{
    public class BaseEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var rootFacility = new NavigationItem("Объекты", typeof(FacilityListView));
            rootFacility.Items.Add(new NavigationItem("Тип объекта", typeof(FacilityTypeListView)));

            var rootCompany = new NavigationItem("Компании", typeof(CompanyListView));
            rootCompany.Items.Add(new NavigationItem("Организационные формы", typeof(CompanyFormListView)));
            rootCompany.Items.Add(new NavigationItem("Сферы деятельности", typeof(ActivityFieldListView)));

            var rootParameter = new NavigationItem("Параметры", typeof(ParameterListView));
            rootParameter.Items.Add(new NavigationItem("Группа параметров", typeof(ParameterGroupListView)));

            var rootProduct = new NavigationItem("Изделия", typeof(ProductListView));
            rootProduct.Items.Add(new NavigationItem("Блоки", typeof(ProductBlockListView)));

            var rootPartPrices = new NavigationItem("Себестоимости", typeof(PartPriceListView));
            rootPartPrices.Items.Add(new NavigationItem("Описание блока", typeof(DescribeProductBlockTaskListView)));
            rootPartPrices.Items.Add(new NavigationItem("Задание на расчет себеистоимости блока", typeof(CalculatePriceTaskListView)));

            var rootContracts = new NavigationItem("Контракты", typeof(ContractListView));

            Items.Add(rootFacility);
            Items.Add(rootCompany);
            Items.Add(rootParameter);
            Items.Add(rootProduct);
            Items.Add(rootPartPrices);
            Items.Add(rootContracts);

            var uiAssembly = typeof(ContractListView).Assembly;
            var views = uiAssembly.GetTypes().Where(x => String.Equals(x.Namespace, "HVTApp.UI.Views") && 
                                                         !x.Name.Contains("Details") &&
                                                         x.Name.Contains("View")).OrderBy(x => x.Name);
            foreach (var view in views)
            {
                Items.Add(new NavigationItem(view.Name, view));
            }

        }
    }
}
