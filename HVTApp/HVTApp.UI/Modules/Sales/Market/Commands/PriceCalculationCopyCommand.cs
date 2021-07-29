using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.View;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class PriceCalculationCopyCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IRegionManager _regionManager;

        public PriceCalculationCopyCommand(Market2ViewModel viewModel, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            _regionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters
            {
                {nameof(PriceCalculation), _viewModel.PriceCalculations.SelectedItem.Entity},
                {nameof(TechnicalRequrementsTask), null}
            });
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.PriceCalculations?.SelectedItem != null;
        }
    }
}