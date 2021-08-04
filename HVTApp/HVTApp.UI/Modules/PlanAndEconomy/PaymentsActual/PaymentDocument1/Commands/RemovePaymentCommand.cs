using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class RemovePaymentCommand : BasePaymentDocumentCommand
    {
        public RemovePaymentCommand(PaymentDocumentViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            ViewModel.UnitOfWork1.Repository<PaymentActual>().Delete(ViewModel.SelectedPayment.PaymentActual.Model);

            //удаление платежа из документа
            var payment = ViewModel.PaymentDocument.Payments.Single(paymentActual => paymentActual.Id == ViewModel.SelectedPayment.PaymentActual.Id);
            ViewModel.PaymentDocument.Payments.Remove(payment);

            //добавление  платежа в список потенциальных
            var potential = ViewModel.SalesUnitWrappers.Single(x => x.PaymentsActual.Select(pa => pa.Id).Contains(payment.Id));
            ViewModel.Potential.Add(potential);

            //удаление платежа из юнита
            var paymentToRemove = potential.PaymentsActual.Single(x => x.Id == payment.Id);
            potential.PaymentsActual.Remove(paymentToRemove);

            ViewModel.Payments.Remove(ViewModel.SelectedPayment);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedPayment != null;
        }
    }
}