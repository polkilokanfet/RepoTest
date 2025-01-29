using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceCalculations.ViewModel.Wrapper
{
    public class PriceCalculationItem2Wrapper : WrapperBase<PriceCalculationItem>
    {
        public bool IsChecked { get; set; } = false;

        public Facility Facility => Model.SalesUnits.FirstOrDefault()?.Facility;
        public Product Product => Model.SalesUnits.FirstOrDefault()?.Product;
        public int Amount => Model.SalesUnits.Count;
        public double? UnitPrice => StructureCosts.Sum(structureCostWrapper => structureCostWrapper.Total);

        #region SimpleProperties

        /// <summary>
        /// Позиция в TeamCenter
        /// </summary>
        public int? PositionInTeamCenter
        {
            get => GetValue<int?>();
            set => SetValue(value);
        }
        public int? PositionInTeamCenterOriginalValue => GetOriginalValue<int?>(nameof(PositionInTeamCenter));
        public bool PositionInTeamCenterIsChanged => GetIsChanged(nameof(PositionInTeamCenter));

        #endregion

        #region ComplexProperties

        public DateTime OrderInTakeDate
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public DateTime RealizationDate
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        /// <summary>
        /// Условия оплаты
        /// </summary>
        public PaymentConditionSetEmptyWrapper PaymentConditionSet
        {
            get => GetWrapper<PaymentConditionSetEmptyWrapper>();
            set => SetComplexValue<PaymentConditionSet, PaymentConditionSetEmptyWrapper>(PaymentConditionSet, value);
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; }

        public IValidatableChangeTrackingCollection<StructureCost2Wrapper> StructureCosts { get; }

        #endregion

        public PriceCalculationItem2Wrapper(PriceCalculationItem model) : base(model)
        {
            #region Initialize

            InitializeComplexProperty(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetEmptyWrapper(Model.PaymentConditionSet));

            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitEmptyWrapper>(Model.SalesUnits.Select(e => new SalesUnitEmptyWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);

            if (Model.StructureCosts == null) throw new ArgumentException("StructureCosts cannot be null");
            StructureCosts = new ValidatableChangeTrackingCollection<StructureCost2Wrapper>(Model.StructureCosts.Select(e => new StructureCost2Wrapper(e)));
            RegisterCollection(StructureCosts, Model.StructureCosts);

            #endregion

            this.SalesUnits.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(string.Empty);
            };

            this.StructureCosts.PropertyChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(UnitPrice));
            };

            this.StructureCosts.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(UnitPrice));
            };
        }

        public PriceCalculationItem2Wrapper(IEnumerable<SalesUnit> salesUnits)
            : this(new PriceCalculationItem {SalesUnits = salesUnits.ToList()})
        {
            this.OrderInTakeDate = DateTime.Today.AddDays(14).SkipWeekend();
            this.RealizationDate = DateTime.Today.AddDays(120).SkipWeekend();
        }
    }
}