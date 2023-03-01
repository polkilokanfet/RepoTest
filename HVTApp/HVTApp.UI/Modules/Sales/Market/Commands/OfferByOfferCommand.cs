using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Views;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class OfferByOfferCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IRegionManager _regionManager;

        public OfferByOfferCommand(Market2ViewModel viewModel, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            var parameters = new NavigationParameters { { nameof(Offer), _viewModel.Offers.SelectedItem.Entity } };
            _regionManager.RequestNavigateContentRegion<OfferView>(parameters);
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.Offers?.SelectedItem != null;
        }
    }
}