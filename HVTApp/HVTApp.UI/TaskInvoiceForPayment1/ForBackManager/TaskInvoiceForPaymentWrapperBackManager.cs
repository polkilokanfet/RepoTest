using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentWrapperBackManager : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemWrapperBackManager>
    {
        public TaskInvoiceForPaymentWrapperBackManager(TaskInvoiceForPayment model) : base(model)
        {
        }

        protected override TaskInvoiceForPaymentItemWrapperBackManager GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemWrapperBackManager(item);
        }
    }
}