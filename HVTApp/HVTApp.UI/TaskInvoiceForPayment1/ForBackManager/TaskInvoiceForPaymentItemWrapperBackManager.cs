using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentItemWrapperBackManager : TaskInvoiceForPaymentItemWrapperBase
    {
        public bool OrdersAreUnique { get; set; }

        #region ComplexProperties

        /// <summary>
        /// Задача ТСП
        /// </summary>
        public PriceEngineeringTaskWrapper PriceEngineeringTask
        {
            get => GetWrapper<PriceEngineeringTaskWrapper>();
            set => SetComplexValue<PriceEngineeringTask, PriceEngineeringTaskWrapper>(PriceEngineeringTask, value);
        }

        /// <summary>
        /// Задача ТСЕ
        /// </summary>
        public TechnicalRequrementsWrapper TechnicalRequrements
        {
            get => GetWrapper<TechnicalRequrementsWrapper>();
            set => SetComplexValue<TechnicalRequrements, TechnicalRequrementsWrapper>(TechnicalRequrements, value);
        }

        #endregion


        public TaskInvoiceForPaymentItemWrapperBackManager(TaskInvoiceForPaymentItem model) : base(model)
        {
        }


        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(PriceEngineeringTask), Model.PriceEngineeringTask == null ? null : new PriceEngineeringTaskWrapper(Model.PriceEngineeringTask));
            InitializeComplexProperty(nameof(TechnicalRequrements), Model.TechnicalRequrements == null ? null : new TechnicalRequrementsWrapper(Model.TechnicalRequrements));
            InitializeComplexProperty(nameof(PaymentCondition), Model.PaymentCondition == null ? null : new PaymentConditionWrapper(Model.PaymentCondition));
        }

    }
}