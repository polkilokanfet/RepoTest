using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Prism.Regions;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculations.Commands
{
    public class EditCalculationCommand : DelegateLogCommand
    {
        private readonly PriceCalculationsViewModel _viewModel;
        private readonly IRegionManager _regionManager;

        public EditCalculationCommand(PriceCalculationsViewModel viewModel, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            _regionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters
            {
                { nameof(PriceCalculation), _viewModel.SelectedItem }
            });
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedItem != null;
        }
    }
}