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
            var rootFacility = new NavigationItem("Объекты", typeof(FacilityLookupListView));
            rootFacility.Items.Add(new NavigationItem("Тип объекта", typeof(FacilityTypeLookupListView)));

            var rootCompany = new NavigationItem("Компании", typeof(CompanyLookupListView));
            rootCompany.Items.Add(new NavigationItem("Организационные формы", typeof(CompanyFormLookupListView)));
            rootCompany.Items.Add(new NavigationItem("Сферы деятельности", typeof(ActivityFieldLookupListView)));

            var rootParameter = new NavigationItem("Параметры", typeof(ParameterLookupListView));
            rootParameter.Items.Add(new NavigationItem("Группа параметров", typeof(ParameterGroupLookupListView)));

            var rootProduct = new NavigationItem("Изделия", typeof(ProductLookupListView));
            rootProduct.Items.Add(new NavigationItem("Блоки", typeof(ProductBlockLookupListView)));

            //var rootPartPrices = new NavigationItem("Себестоимости", typeof(PartPriceLookupListView));
            //rootPartPrices.Items.Add(new NavigationItem("Описание блока", typeof(DescribeProductBlockTaskLookupListView)));
            //rootPartPrices.Items.Add(new NavigationItem("Задание на расчет себеистоимости блока", typeof(CalculatePriceTaskLookupListView)));

            var rootContracts = new NavigationItem("Контракты", typeof(ContractLookupListView));

            Items.Add(rootFacility);
            Items.Add(rootCompany);
            Items.Add(rootParameter);
            Items.Add(rootProduct);
            //Items.Add(rootPartPrices);
            Items.Add(rootContracts);

            var uiAssembly = typeof(ContractLookupListView).Assembly;
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
