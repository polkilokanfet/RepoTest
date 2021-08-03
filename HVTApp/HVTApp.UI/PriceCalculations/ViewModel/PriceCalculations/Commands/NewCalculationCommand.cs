using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Prism.Regions;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculations.Commands
{
    public class NewCalculationCommand : DelegateLogCommand
    {
        private readonly IRegionManager _regionManager;

        public NewCalculationCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            _regionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters
            {
                { nameof(PriceCalculation), new PriceCalculation() }
            });
        }
    }
}