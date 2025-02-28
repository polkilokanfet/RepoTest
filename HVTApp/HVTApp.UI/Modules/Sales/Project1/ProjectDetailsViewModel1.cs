using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectDetailsViewModel1 : BaseDetailsViewModel<ProjectWrapper1, Project, AfterSaveProjectEvent>
    {
        public ProjectDetailsViewModel1(IUnityContainer container) : base(container)
        {
        }
    }
}