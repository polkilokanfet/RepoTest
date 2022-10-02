using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class RestPaymentCommand : BasePaymentDocumentCommand
    {
        public RestPaymentCommand(PaymentDocumentViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            ViewModel.Item.Payments.ForEach(payment => payment.SetRestPay());
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.Item != null;
        }
    }
}