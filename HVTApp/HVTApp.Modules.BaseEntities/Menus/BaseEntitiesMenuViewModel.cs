using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Views;
using HVTApp.UI;

namespace HVTApp.Modules.BaseEntities.Menus
{
    public class BaseEntitiesMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            var views = typeof(CompanyLookupListView).Assembly.GetTypes()
                .Where(x => string.Equals(x.Namespace, typeof(CompanyLookupListView).Namespace) && 
                            x.Name.Contains("LookupListView")).OrderBy(x => x.DesignationSingle()).ToList();

            foreach (var view in views)
            {
                //добавление видов в соответствии с правами доступа
                if (view.GetAllowEditRoles().Contains(GlobalAppProperties.User.RoleCurrent))
                {
                    Items.AddToNavigate(view);
                }
            }
        }
    }
}
