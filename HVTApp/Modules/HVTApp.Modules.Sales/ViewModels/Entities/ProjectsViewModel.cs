using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : BaseListViewModel<ProjectLookup, Project, ProjectDetailsViewModel>
    {
        public ProjectsViewModel(IUnityContainer container, IProjectLookupDataService projectLookupDataService) : base(container, projectLookupDataService)
        {
        }
    }
}
