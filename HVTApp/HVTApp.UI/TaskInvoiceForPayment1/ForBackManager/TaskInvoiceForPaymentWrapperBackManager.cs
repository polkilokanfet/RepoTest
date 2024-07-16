using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentWrapperBackManager : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemWrapperBackManager>
    {

        /// <summary>
        /// Финиш задачи
        /// </summary>
        public System.DateTime? MomentFinish
        {
            get => Model.MomentFinish;
            set => SetValue(value);
        }
        public System.DateTime? MomentFinishOriginalValue => GetOriginalValue<System.DateTime?>(nameof(MomentFinish));
        public bool MomentFinishIsChanged => GetIsChanged(nameof(MomentFinish));

        public TaskInvoiceForPaymentWrapperBackManager(TaskInvoiceForPayment model) : base(model)
        {
        }

        protected override TaskInvoiceForPaymentItemWrapperBackManager GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemWrapperBackManager(item);
        }
    }
}