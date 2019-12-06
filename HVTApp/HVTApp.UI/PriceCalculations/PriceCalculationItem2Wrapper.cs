using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.PriceCalculations
{
    public class PriceCalculationItem2Wrapper : WrapperBase<PriceCalculationItem>
    {
        public Project Project => Model.SalesUnits.FirstOrDefault()?.Project;
        public Facility Facility => Model.SalesUnits.FirstOrDefault()?.Facility;
        public Product Product => Model.SalesUnits.FirstOrDefault()?.Product;
        public int Amount => Model.SalesUnits.Count;
        public double? UnitPrice => StructureCosts.Sum(x => x.Total);
        public DateTime? OrderInTakeDate => Model.SalesUnits.FirstOrDefault()?.OrderInTakeDate;
        public DateTime? RealizationDate => Model.SalesUnits.FirstOrDefault()?.RealizationDateCalculated;
        public PaymentConditionSet PaymentConditionSet => Model.SalesUnits.FirstOrDefault()?.PaymentConditionSet;

        public PriceCalculationItem2Wrapper(PriceCalculationItem model) : base(model)
        {
            this.SalesUnits.CollectionChanged += (sender, args) => { OnPropertyChanged(string.Empty); };

            this.StructureCosts.PropertyChanged += (sender, args) =>
            {
                this.OnPropertyChanged(nameof(PriceCalculationItem2Wrapper.UnitPrice));
            };

            this.StructureCosts.CollectionChanged += (sender, args) =>
            {
                this.OnPropertyChanged(nameof(PriceCalculationItem2Wrapper.UnitPrice));
            };
        }

        #region SimpleProperties

        public System.Guid PriceCalculationId
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid PriceCalculationIdOriginalValue => GetOriginalValue<System.Guid>(nameof(PriceCalculationId));
        public bool PriceCalculationIdIsChanged => GetIsChanged(nameof(PriceCalculationId));

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