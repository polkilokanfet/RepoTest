using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : ProjectLookupListViewModel
    {
        public ProjectsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override async Task<IEnumerable<ProjectLookup>> GetLookups()
        {
            return (await base.GetLookups()).Where(x => x.Manager.Id == CommonOptions.User.Id);
        }
    }
}
