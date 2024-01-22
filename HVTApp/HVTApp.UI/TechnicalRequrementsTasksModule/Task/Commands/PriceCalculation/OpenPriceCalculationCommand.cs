using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class OpenPriceCalculationCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public OpenPriceCalculationCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters
            {
                {nameof(PriceCalculation), ViewModel.SelectedCalculation}
            });
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedCalculation != null;
        }
    }
}