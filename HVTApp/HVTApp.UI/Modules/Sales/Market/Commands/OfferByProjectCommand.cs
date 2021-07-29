using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Views;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class OfferByProjectCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IRegionManager _regionManager;

        public OfferByProjectCommand(Market2ViewModel viewModel, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            var prms = new NavigationParameters { { "project", _viewModel.SelectedProjectItem.Project } };
            _regionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}