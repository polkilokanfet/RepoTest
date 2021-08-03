using HVTApp.Infrastructure.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public abstract class BasePriceCalculationCommand : DelegateLogCommand
    {
        protected PriceCalculationViewModel ViewModel { get; }
        protected IUnityContainer Container { get; }
        protected IMessageService MessageService { get; }

        protected BasePriceCalculationCommand(PriceCalculationViewModel viewModel, IUnityContainer container)
        {
            ViewModel = viewModel;
            Container = container;
            MessageService = Container.Resolve<IMessageService>();
        }

        protected abstract override void ExecuteMethod();
    }
}