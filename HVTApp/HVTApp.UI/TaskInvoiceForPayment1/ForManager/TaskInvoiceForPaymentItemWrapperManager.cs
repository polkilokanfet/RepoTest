using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentItemWrapperManager : TaskInvoiceForPaymentItemWrapperBase
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

        public TaskInvoiceForPaymentItemWrapperManager(TaskInvoiceForPaymentItem model) : base(model)
        {
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.PaymentCondition))
                {
                    RaisePropertyChanged(nameof(this.CostInvoice));
                    RaisePropertyChanged(nameof(this.SumInvoice));
                    RaisePropertyChanged(nameof(this.SumWithVatInvoice));
                }
            };
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(PaymentCondition), Model.PaymentCondition == null ? null : new PaymentConditionEmptyWrapper(Model.PaymentCondition));
        }
    }
}