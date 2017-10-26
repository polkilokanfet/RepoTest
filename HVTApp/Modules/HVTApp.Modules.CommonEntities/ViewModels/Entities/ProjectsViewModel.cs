using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ProjectsViewModel : BaseListViewModel<ProjectLookup, Project, ProjectDetailsViewModel, AfterSaveProjectEvent>
    {
        public ProjectsViewModel(IUnityContainer container, IProjectLookupDataService projectLookupDataService) : base(container, projectLookupDataService)
        {
        }
    }
}
