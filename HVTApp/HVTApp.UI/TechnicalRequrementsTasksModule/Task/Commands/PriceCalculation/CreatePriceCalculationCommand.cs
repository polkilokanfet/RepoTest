using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class CreatePriceCalculationCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public CreatePriceCalculationCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(
                new NavigationParameters
                {
                    {nameof(TechnicalRequrementsTask), ViewModel.TechnicalRequrementsTaskWrapper.Model}
                });

        }
    }
}