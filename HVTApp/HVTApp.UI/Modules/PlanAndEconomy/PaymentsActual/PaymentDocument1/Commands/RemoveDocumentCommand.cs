using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class RemoveDocumentCommand : BasePaymentDocumentCommand
    {
        public RemoveDocumentCommand(PaymentDocumentViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var messageService = Container.Resolve<IMessageService>();
            var dr = messageService.ShowYesNoMessageDialog("��������", "�� �������, ��� ������ ������� ���� ��������� ��������?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;

            ViewModel.UnitOfWork1.Repository<PaymentActual>().DeleteRange(ViewModel.Payments.Select(payment => payment.PaymentActual.Model));
            ViewModel.UnitOfWork1.Repository<PaymentDocument>().Delete(ViewModel.PaymentDocument.Model);
            ViewModel.UnitOfWork1.SaveChanges();

            ViewModel.GoBackCommand.Execute(null);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.UnitOfWork1.Repository<PaymentDocument>().GetById(ViewModel.PaymentDocument.Id) != null;
        }
    }
}