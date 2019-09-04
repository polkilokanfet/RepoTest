using System.Windows.Input;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.Infrastructure
{
    public class ViewModelBase : BindableBase
    {
        #region Fields

        protected readonly IUnityContainer Container;
        protected readonly IRegionManager RegionManager;
        protected IUnitOfWork UnitOfWork;

        #endregion

        #region Commands

        /// <summary>
        /// Команда перехода к следующему виду
        /// </summary>
        public ICommand GoForwardCommand { get; }

        /// <summary>
        /// Команда перехода к предыдущему виду
        /// </summary>
        public ICommand GoBackCommand { get; }

        #endregion

        public ViewModelBase(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
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

        protected virtual void GoBackCommand_Execute()
        {
            RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.GoBack();
        }

        private bool GoBackCommand_CanExecute()
        {
            return RegionManager.Regions[RegionNames.ContentRegion].NavigationService.Journal.CanGoBack;
        }
    }
}