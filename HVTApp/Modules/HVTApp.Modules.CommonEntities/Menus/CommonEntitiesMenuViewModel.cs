using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Views;

namespace HVTApp.Modules.CommonEntities.Menus
{
    public class CommonEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            NavigationItem root = new NavigationItem("Companies", typeof(CompaniesView));
            root.Items.Add(new NavigationItem("CompanyForms", typeof(CompanyFormsView)));

            Items.Add(root);
        }
    }
}
