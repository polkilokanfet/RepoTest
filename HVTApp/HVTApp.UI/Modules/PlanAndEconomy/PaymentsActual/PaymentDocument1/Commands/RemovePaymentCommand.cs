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

            //����������  ������� � ������ �������������
            ViewModel.Potential.Insert(0, ViewModel.UnitOfWork1.Repository<SalesUnit>().GetById(payment.SalesUnitId));

            //�������� ������� �� ���������
            ViewModel.Item.Payments.Remove(ViewModel.SelectedPayment);

            //�������� ������� �� ����� ������������� ���� � PaymentDocumentWrapper1

            //�������� ������� �� �����������
            if (ViewModel.UnitOfWork1.Repository<PaymentActual>().GetById(payment.Model.Id) != null)
                ViewModel.UnitOfWork1.Repository<PaymentActual>().Delete(payment.Model);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedPayment != null;
        }
    }
}