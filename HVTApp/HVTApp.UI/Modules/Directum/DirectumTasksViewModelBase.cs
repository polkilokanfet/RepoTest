using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    public abstract class DirectumTasksViewModelBase : ViewModelBase
    {
        public ICommand ReloadCommand { get; }
        public ICommand CreateDirectumTaskCommand { get; }
        public ICommand OpenDirectumTaskCommand { get; protected set; }

        protected DirectumTasksViewModelBase(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);

            CreateDirectumTaskCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters());
                });
        }

        protected abstract void Load();
    }
}