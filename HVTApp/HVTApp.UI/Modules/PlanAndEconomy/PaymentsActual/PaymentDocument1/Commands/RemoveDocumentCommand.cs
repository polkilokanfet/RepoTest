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
            var dr = messageService.ShowYesNoMessageDialog("Удаление", "Вы уверены, что хотите удалить весь платежный документ?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;

            foreach (var paymentActualWrapper2 in ViewModel.Item.Payments.ToList())
            {
                ViewModel.Item.Payments.Remove(paymentActualWrapper2);
                ViewModel.UnitOfWork1.Repository<PaymentActual>().Delete(paymentActualWrapper2.Model);
            }

            ViewModel.UnitOfWork1.Repository<PaymentDocument>().Delete(ViewModel.Item.Model);
            ViewModel.UnitOfWork1.SaveChanges();

            ViewModel.GoBackCommand.Execute(null);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.UnitOfWork1.Repository<PaymentDocument>().GetById(ViewModel.Item.Model.Id) != null;
        }
    }
}