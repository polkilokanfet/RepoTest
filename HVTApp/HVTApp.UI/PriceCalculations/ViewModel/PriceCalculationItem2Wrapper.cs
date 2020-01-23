using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class PriceCalculationItem2Wrapper : WrapperBase<PriceCalculationItem>
    {
        public bool IsChecked { get; set; } = false;

        public Project Project => Model.SalesUnits.FirstOrDefault()?.Project;
        public Facility Facility => Model.SalesUnits.FirstOrDefault()?.Facility;
        public Product Product => Model.SalesUnits.FirstOrDefault()?.Product;
        public int Amount => Model.SalesUnits.Count;
        public double? UnitPrice => StructureCosts.Sum(x => x.Total);

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
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
        public Guid PriceCalculationIdOriginalValue => GetOriginalValue<Guid>(nameof(PriceCalculationId));
        public bool PriceCalculationIdIsChanged => GetIsChanged(nameof(PriceCalculationId));

        #endregion

        #region ComplexProperties

        public DateTime? OrderInTakeDate
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public DateTime? RealizationDate
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public PaymentConditionSet PaymentConditionSet
        {
            get { return GetValue<PaymentConditionSet>(); }
            set { SetValue(value); }
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<SalesUnit2Wrapper> SalesUnits { get; private set; }

        public IValidatableChangeTrackingCollection<StructureCostWrapper> StructureCosts { get; private set; }

        #endregion

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnit2Wrapper>(Model.SalesUnits.Select(e => new SalesUnit2Wrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);

            if (Model.StructureCosts == null) throw new ArgumentException("StructureCosts cannot be null");
            StructureCosts = new ValidatableChangeTrackingCollection<StructureCostWrapper>(Model.StructureCosts.Select(e => new StructureCostWrapper(e)));
            RegisterCollection(StructureCosts, Model.StructureCosts);
        }

    }
}