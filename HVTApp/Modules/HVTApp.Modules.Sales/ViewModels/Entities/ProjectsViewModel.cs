using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : BaseListViewModel<ProjectLookup, Project, ProjectDetailsViewModel, AfterSaveProjectEvent>
    {
        public ProjectsViewModel(IUnityContainer container, IProjectLookupDataService projectLookupDataService) : base(container, projectLookupDataService)
        {
        }
    }
}
