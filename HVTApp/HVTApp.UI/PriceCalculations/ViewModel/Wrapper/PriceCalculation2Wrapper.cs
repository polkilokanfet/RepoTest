using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class PriceCalculation2Wrapper : WrapperBase<PriceCalculation>
    {
        public PriceCalculation2Wrapper(PriceCalculation model) : base(model) { }

        #region SimpleProperties

        public DateTime? TaskOpenMoment
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? TaskOpenMomentOriginalValue => GetOriginalValue<DateTime?>(nameof(TaskOpenMoment));
        public bool TaskOpenMomentIsChanged => GetIsChanged(nameof(TaskOpenMoment));

        public DateTime? TaskCloseMoment
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? TaskCloseMomentOriginalValue => GetOriginalValue<DateTime?>(nameof(TaskCloseMoment));
        public bool TaskCloseMomentIsChanged => GetIsChanged(nameof(TaskCloseMoment));

        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        public bool IsNeedExcelFile
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool IsNeedExcelFileOriginalValue => GetOriginalValue<bool>(nameof(IsNeedExcelFile));
        public bool IsNeedExcelFileIsChanged => GetIsChanged(nameof(IsNeedExcelFile));

        #endregion

        #region ComplexProperties

        public UserWrapper Initiator
        {
            get => GetWrapper<UserWrapper>();
            set => SetComplexValue<User, UserWrapper>(Initiator, value);
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<PriceCalculationItem2Wrapper> PriceCalculationItems { get; private set; }
        public IValidatableChangeTrackingCollection<PriceCalculationFileWrapper> Files { get; private set; }

        #endregion

        #region GetProperties

        public string Name => GetValue<string>();

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Initiator), Model.Initiator == null ? null : new UserWrapper(Model.Initiator));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PriceCalculationItems == null) throw new ArgumentException("PriceCalculationItems cannot be null");
            PriceCalculationItems = new ValidatableChangeTrackingCollection<PriceCalculationItem2Wrapper>(Model.PriceCalculationItems.Select(e => new PriceCalculationItem2Wrapper(e)));
            RegisterCollection(PriceCalculationItems, Model.PriceCalculationItems);

            if (Model.Files == null) throw new ArgumentException("Files cannot be null");
            Files = new ValidatableChangeTrackingCollection<PriceCalculationFileWrapper>(Model.Files.Select(e => new PriceCalculationFileWrapper(e)));
            RegisterCollection(Files, Model.Files);
        }
    }
}