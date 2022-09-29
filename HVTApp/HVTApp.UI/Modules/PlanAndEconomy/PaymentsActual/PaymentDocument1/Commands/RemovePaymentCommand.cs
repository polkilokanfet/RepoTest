using HVTApp.Infrastructure.Extansions;
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
            var payment = ViewModel.SelectedPayment;

            //добавление  платежа в список потенциальных
            ViewModel.Potential.Insert(0, ViewModel.UnitOfWork1.Repository<SalesUnit>().GetById(payment.SalesUnitId));

            //удаление платежа из документа
            ViewModel.Item.Payments.Remove(ViewModel.SelectedPayment);

            //удаление платежа из юнита автоматически идет в PaymentDocumentWrapper1

            //удаления платежа из репозитория
            if (ViewModel.UnitOfWork1.Repository<PaymentActual>().GetById(payment.Model.Id) != null)
                ViewModel.UnitOfWork1.Repository<PaymentActual>().Delete(payment.Model);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedPayment != null;
        }
    }
}