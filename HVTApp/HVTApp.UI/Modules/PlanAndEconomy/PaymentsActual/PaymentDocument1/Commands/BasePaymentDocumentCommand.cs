using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public abstract class BasePaymentDocumentCommand : DelegateLogCommand
    {
        protected PaymentDocumentViewModel ViewModel { get; }
        protected IUnityContainer Container { get; }

        protected BasePaymentDocumentCommand(PaymentDocumentViewModel viewModel, IUnityContainer container)
        {
            ViewModel = viewModel;
            Container = container;
        }

        protected abstract override void ExecuteMethod();
    }
}