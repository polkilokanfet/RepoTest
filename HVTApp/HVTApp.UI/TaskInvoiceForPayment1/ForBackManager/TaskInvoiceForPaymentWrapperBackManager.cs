using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentWrapperBackManager : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemViewModelBackManager>
    {
        public TaskInvoiceForPaymentWrapperBackManager(TaskInvoiceForPayment model) : base(model)
        {
        }

        protected override TaskInvoiceForPaymentItemViewModelBackManager GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemViewModelBackManager(item);
        }
    }
}