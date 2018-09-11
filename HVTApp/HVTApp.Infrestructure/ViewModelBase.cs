using System.Windows.Input;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.Infrastructure
{
    public class ViewModelBase : BindableBase
    {
        protected readonly IRegionManager RegionManager;

        protected readonly IUnityContainer Container;

        public ICommand GoForwardCommand { get; }
        public ICommand GoBackCommand { get; }

        public ViewModelBase(IUnityContainer container)
        {
            Container = container;
            RegionManager = Container.Resolve<IRegionManager>();
            GoForwardCommand = new DelegateCommand(GoForwardCommand_Execute, GoForwardCommand_CanExecute);
            GoBackCommand = new DelegateCommand(GoBackCommand_Execute, GoBackCommand_CanExecute);
        }

        private void GoForwardCommand_Execute()
        {
            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoForward();
        }

        private bool GoForwardCommand_CanExecute()
        {
            return RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.CanGoForward;
        }

        private void GoBackCommand_Execute()
        {
            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }

        private bool GoBackCommand_CanExecute()
        {
            return RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.CanGoBack;
        }
    }
}