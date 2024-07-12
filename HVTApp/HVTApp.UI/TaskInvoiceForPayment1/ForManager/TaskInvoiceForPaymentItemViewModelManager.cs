using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentItemViewModelManager : TaskInvoiceForPaymentItemViewModelBase
    {
        #region ComplexProperties

        /// <summary>
        /// Связанное условие платежа
        /// </summary>
        public PaymentConditionEmptyWrapper PaymentCondition
        {
            get => GetWrapper<PaymentConditionEmptyWrapper>();
            set => SetComplexValue<PaymentCondition, PaymentConditionEmptyWrapper>(PaymentCondition, value);
        }

        #endregion

        public TaskInvoiceForPaymentItemViewModelManager(TaskInvoiceForPaymentItem model) : base(model)
        { }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(PaymentCondition), Model.PaymentCondition == null ? null : new PaymentConditionEmptyWrapper(Model.PaymentCondition));
        }
    }
}