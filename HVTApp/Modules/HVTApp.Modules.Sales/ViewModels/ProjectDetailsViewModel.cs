using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper, Project>
    {
        public ProjectDetailsViewModel(ProjectWrapper item) : base(item)
        {
        }
    }
}