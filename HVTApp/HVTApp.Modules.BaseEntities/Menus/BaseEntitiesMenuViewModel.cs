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
            var views = typeof(ContractLookupListView).Assembly.GetTypes()
                .Where(x => String.Equals(x.Namespace, typeof(CompanyLookupListView).Namespace) && 
                !x.Name.Contains("Details") && x.Name.Contains("ListView")).OrderBy(x => x.Name);

            foreach (var view in views)
            {
                Items.Add(new NavigationItem(view.Name, view));
            }

        }
    }
}
