using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentItemViewModelBackManager : TaskInvoiceForPaymentItemViewModelBase
    {
        public bool OrdersAreUnique { get; set; }

        public TaskInvoiceForPaymentItemViewModelBackManager(TaskInvoiceForPaymentItem model) : base(model)
        {
        }
    }
}