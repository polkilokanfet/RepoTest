using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : ProjectLookupListViewModel
    {
        public ProjectsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}
