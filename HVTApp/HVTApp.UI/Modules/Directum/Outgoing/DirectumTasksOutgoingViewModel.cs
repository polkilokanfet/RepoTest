using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTasksOutgoingViewModel : ViewModelBase
    {
        public ICommand CreateDirectumTaskCommand { get; }

        public DirectumTasksOutgoingViewModel(IUnityContainer container) : base(container)
        {
            CreateDirectumTaskCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters());
                });
        }
    }
}