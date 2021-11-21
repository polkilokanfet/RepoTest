using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;

namespace HVTApp.UI.PriceCalculations.ViewModel.Wrapper
{
    public class PriceCalculationItem2Wrapper : WrapperBase<PriceCalculationItem>
    {
        public bool IsChecked { get; set; } = false;

        public Project Project => Model.SalesUnits.FirstOrDefault()?.Project;
        public Facility Facility => Model.SalesUnits.FirstOrDefault()?.Facility;
        public Product Product => Model.SalesUnits.FirstOrDefault()?.Product;
        public int Amount => Model.SalesUnits.Count;
        public double? UnitPrice => StructureCosts.Sum(structureCostWrapper => structureCostWrapper.Total);

        public PriceCalculationItem2Wrapper(PriceCalculationItem model) : base(model)
        {

            this.SalesUnits.CollectionChanged += (sender, args) =>
            {
                if (!Model.OrderInTakeDate.HasValue)
                    OrderInTakeDate = Model.SalesUnits.FirstOrDefault()?.OrderInTakeDate;

                if (!Model.RealizationDate.HasValue)
                    RealizationDate = Model.SalesUnits.FirstOrDefault()?.RealizationDateCalculated;

                if (Model.PaymentConditionSet == null)
                    PaymentConditionSet = Model.SalesUnits.FirstOrDefault()?.PaymentConditionSet;

                OnPropertyChanged(string.Empty);
            };

            this.StructureCosts.PropertyChanged += (sender, args) =>
            {
                this.OnPropertyChanged(nameof(UnitPrice));
            };

            this.StructureCosts.CollectionChanged += (sender, args) =>
            {
                this.OnPropertyChanged(nameof(UnitPrice));
            };
        }

        #region SimpleProperties

        public Guid PriceCalculationId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }
        public Guid PriceCalculationIdOriginalValue => GetOriginalValue<Guid>(nameof(PriceCalculationId));
        public bool PriceCalculationIdIsChanged => GetIsChanged(nameof(PriceCalculationId));

        #endregion

        #region ComplexProperties

        public DateTime? OrderInTakeDate
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }

        public DateTime? RealizationDate
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }

        public PaymentConditionSet PaymentConditionSet
        {
            get => GetValue<PaymentConditionSet>();
            set => SetValue(value);
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; private set; }

        public IValidatableChangeTrackingCollection<StructureCostWrapper> StructureCosts { get; private set; }

        #endregion

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitEmptyWrapper>(Model.SalesUnits.Select(e => new SalesUnitEmptyWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);

            if (Model.StructureCosts == null) throw new ArgumentException("StructureCosts cannot be null");
            StructureCosts = new ValidatableChangeTrackingCollection<StructureCostWrapper>(Model.StructureCosts.Select(e => new StructureCostWrapper(e)));
            RegisterCollection(StructureCosts, Model.StructureCosts);
        }

    }
}