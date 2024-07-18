using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentItemWrapperBackManager : TaskInvoiceForPaymentItemWrapperBase
    {
        public override string Order
        {
            get => this.SalesUnits.Where(x => x.Order != null).Select(x => x.Order.Number).Distinct().ToStringEnum();
            set
            {
                foreach (var salesUnit in this.SalesUnits)
                {
                    salesUnit.Order.Number = value;
                }
            }
        }

        public override string OrderPositions
        {
            get
            {
                return this.SalesUnits.Select(x => x.OrderPosition).GetOrderPositions();
            }
            set
            {
                foreach (var salesUnit in this.SalesUnits)
                {
                    salesUnit.OrderPosition = value;
                }
            }
        }

        #region ComplexProperties

        public override IValidatableChangeTrackingCollection<SalesUnitWrapperTip> SalesUnits =>
            PriceEngineeringTask != null
                ? PriceEngineeringTask.SalesUnits
                : TechnicalRequrements.SalesUnits;

        /// <summary>
        /// Задача ТСП
        /// </summary>
        public PriceEngineeringTaskWrapperTip PriceEngineeringTask
        {
            get => GetWrapper<PriceEngineeringTaskWrapperTip>();
            set => SetComplexValue<PriceEngineeringTask, PriceEngineeringTaskWrapperTip>(PriceEngineeringTask, value);
        }

        /// <summary>
        /// Задача ТСЕ
        /// </summary>
        public TechnicalRequrementsWrapperTip TechnicalRequrements
        {
            get => GetWrapper<TechnicalRequrementsWrapperTip>();
            set => SetComplexValue<TechnicalRequrements, TechnicalRequrementsWrapperTip>(TechnicalRequrements, value);
        }

        #endregion
        
        public TaskInvoiceForPaymentItemWrapperBackManager(TaskInvoiceForPaymentItem model) : base(model)
        {
            foreach (var salesUnit in this.SalesUnits)
            {
                salesUnit.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(salesUnit.OrderPosition))
                    {
                        RaisePropertyChanged(nameof(OrderPositions));
                        this.Validate();
                    }
                };

                salesUnit.OrderChangedEvent += order =>
                {
                    order.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(order.Number))
                        {
                            RaisePropertyChanged(nameof(Order));
                            this.Validate();
                        }
                    };
                };
            }
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(PriceEngineeringTask), Model.PriceEngineeringTask == null ? null : new PriceEngineeringTaskWrapperTip(Model.PriceEngineeringTask));
            InitializeComplexProperty(nameof(TechnicalRequrements), Model.TechnicalRequrements == null ? null : new TechnicalRequrementsWrapperTip(Model.TechnicalRequrements));
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(this.Order))
                yield return new ValidationResult("Не назначен ни один заказ.", new[] { nameof(Order) });

            if (string.IsNullOrWhiteSpace(this.OrderPositions))
                yield return new ValidationResult("Не назначен ни одина позиция заказа.", new[] { nameof(Order) });
        }
    }
}