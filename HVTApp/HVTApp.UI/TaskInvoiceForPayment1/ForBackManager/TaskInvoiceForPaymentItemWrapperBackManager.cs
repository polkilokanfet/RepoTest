using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentItemWrapperBackManager : TaskInvoiceForPaymentItemWrapperBase
    {
        public bool OrdersAreUnique { get; set; }

        #region ComplexProperties

        public override IValidatableChangeTrackingCollection<SalesUnitWrapperTip> SalesUnits =>
            PriceEngineeringTask != null
                ? PriceEngineeringTask.SalesUnits
                : TechnicalRequrements.SalesUnits;

        /// <summary>
        /// Задача ТСП
        /// </summary>
        private PriceEngineeringTaskWrapperTip PriceEngineeringTask
        {
            get => GetWrapper<PriceEngineeringTaskWrapperTip>();
            set => SetComplexValue<PriceEngineeringTask, PriceEngineeringTaskWrapperTip>(PriceEngineeringTask, value);
        }

        /// <summary>
        /// Задача ТСЕ
        /// </summary>
        private TechnicalRequrementsWrapperTip TechnicalRequrements
        {
            get => GetWrapper<TechnicalRequrementsWrapperTip>();
            set => SetComplexValue<TechnicalRequrements, TechnicalRequrementsWrapperTip>(TechnicalRequrements, value);
        }

        #endregion
        
        public TaskInvoiceForPaymentItemWrapperBackManager(TaskInvoiceForPaymentItem model) : base(model)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(PriceEngineeringTask), Model.PriceEngineeringTask == null ? null : new PriceEngineeringTaskWrapperTip(Model.PriceEngineeringTask));
            InitializeComplexProperty(nameof(TechnicalRequrements), Model.TechnicalRequrements == null ? null : new TechnicalRequrementsWrapperTip(Model.TechnicalRequrements));
        }
    }
}