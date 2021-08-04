using System.Linq;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class AddPaymentCommand : BasePaymentDocumentCommand
    {
        public AddPaymentCommand(PaymentDocumentViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var sum = ViewModel.DockSumWithVat;
            var date = ViewModel.DockDate;

            var selectedUnits = ViewModel.SelectedPotentialUnits.Cast<SalesUnitPaymentWrapper>().ToList();

            foreach (var selectedUnit in selectedUnits)
            {
                var payment = new Payment(selectedUnit);
                ViewModel.PaymentDocument.Payments.Add(payment.PaymentActual);
                ViewModel.Payments.Add(payment);
                ViewModel.Potential.Remove(selectedUnit);
            }
            ViewModel.SelectedPotentialUnits = null;

            ViewModel.DockSumWithVat = sum;
            ViewModel.DockDate = date;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedPotentialUnits != null;
        }
    }
}