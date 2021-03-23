using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    public abstract class DirectumTasksViewModelBase : LoadableExportableExpandCollapseViewModel
    {
        public ICommand CreateDirectumTaskCommand { get; }
        public ICommand OpenDirectumTaskCommand { get; protected set; }

        protected DirectumTasksViewModelBase(IUnityContainer container) : base(container)
        {
            CreateDirectumTaskCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters());
                });
        }
    }
}