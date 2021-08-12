using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class CopyPriceCalculationCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public CopyPriceCalculationCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(
                new NavigationParameters
                {
                    {nameof(PriceCalculation), ViewModel.SelectedCalculation},
                    {nameof(TechnicalRequrementsTask), ViewModel.TechnicalRequrementsTaskWrapper.Model}
                });
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedCalculation != null;
        }
    }
}