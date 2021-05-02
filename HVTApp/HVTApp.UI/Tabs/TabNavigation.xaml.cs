using HVTApp.Infrastructure;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Tabs
{
    public partial class TabNavigation
    {
        protected readonly IRegionManager RegionManager;

        protected readonly IUnityContainer Container;

        public DelegateLogCommand GoForwardCommand { get; }
        public DelegateLogCommand GoBackCommand { get; }

        public TabNavigation(IUnityContainer container)
        {
            InitializeComponent();
            Container = container;
            RegionManager = Container.Resolve<IRegionManager>();
            GoForwardCommand = new DelegateLogCommand(GoForwardCommand_Execute, GoForwardCommand_CanExecute);
            GoBackCommand = new DelegateLogCommand(GoBackCommand_Execute, GoBackCommand_CanExecute);
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
