using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    public abstract class DirectumTasksViewModelBase : LoadableExportableExpandCollapseViewModel
    {
        public DelegateLogCommand CreateDirectumTaskCommand { get; }
        public DelegateLogCommand OpenDirectumTaskCommand { get; protected set; }

        protected DirectumTasksViewModelBase(IUnityContainer container) : base(container)
        {
            CreateDirectumTaskCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters());
                });
        }
    }
}