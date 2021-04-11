using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Modules.Sales.Views;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class ProjectNewCommand : DelegateCommandBase
    {
        private readonly IRegionManager _regionManager;
        public ProjectNewCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        protected override void Execute(object parameter)
        {
            _regionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }
        protected override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}