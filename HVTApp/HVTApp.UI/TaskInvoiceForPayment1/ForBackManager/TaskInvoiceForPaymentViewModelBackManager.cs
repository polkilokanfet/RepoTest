using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.Base;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentViewModelBackManager :
        TaskInvoiceForPaymentViewModelBase<TaskInvoiceForPaymentWrapperBackManager, TaskInvoiceForPaymentItemViewModelBackManager>
    {
        public TaskInvoiceForPaymentViewModelBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override TaskInvoiceForPaymentWrapperBackManager GetTask(TaskInvoiceForPayment taskInvoice)
        {
            return new TaskInvoiceForPaymentWrapperBackManager(taskInvoice);
        }
    }
}