using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class EditPriceCalculationCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IRegionManager _regionManager;

        public EditPriceCalculationCommand(Market2ViewModel viewModel, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            _regionManager.RequestNavigateContentRegion<PriceCalculations.View.PriceCalculationView>(
                new NavigationParameters
                {
                    {nameof(PriceCalculation), _viewModel.PriceCalculations.SelectedItem.Entity}
                });
        }
    }
}