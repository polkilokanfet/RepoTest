using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForPlanMaker
{
    public class TaskInvoiceForPaymentWrapperPlanMaker : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemWrapperPlanMaker>
    {
        public System.DateTime? MomentFinishByPlanMaker
        {
            get => Model.MomentFinishByPlanMaker;
            set => SetValue(value);
        }
        public System.DateTime? MomentFinishByPlanMakerOriginalValue => GetOriginalValue<System.DateTime?>(nameof(MomentFinishByPlanMaker));
        public bool MomentFinishByPlanMakerIsChanged => GetIsChanged(nameof(MomentFinishByPlanMaker));

        public TaskInvoiceForPaymentWrapperPlanMaker(TaskInvoiceForPayment model) : base(model)
        {
        }

        protected override TaskInvoiceForPaymentItemWrapperPlanMaker GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemWrapperPlanMaker(item);
        }
    }
}