using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.UI.Views;
using HVTApp.UI;

namespace HVTApp.Modules.BaseEntities.Menus
{
    public class BaseEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var views = typeof(ContractLookupListView).Assembly.GetTypes()
                .Where(x => 
                            String.Equals(x.Namespace, typeof(CompanyLookupListView).Namespace) && 
                            x.Name.Contains("LookupListView")).OrderBy(x => x.DesignationPlural());

            foreach (var view in views)
            {
                Items.AddToNavigate(view);
                //Items.Add(new NavigationItem(view.Name, view));
            }

        }
    }
}
