using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper;

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

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<PriceCalculationItem2Wrapper> PriceCalculationItems { get; private set; }

        #endregion

        #region GetProperties

        public string Name => GetValue<string>();

        #endregion

        protected override void InitializeCollectionProperties()
        {
            if (Model.PriceCalculationItems == null) throw new ArgumentException("PriceCalculationItems cannot be null");
            PriceCalculationItems = new ValidatableChangeTrackingCollection<PriceCalculationItem2Wrapper>(Model.PriceCalculationItems.Select(e => new PriceCalculationItem2Wrapper(e)));
            RegisterCollection(PriceCalculationItems, Model.PriceCalculationItems);
        }
    }
}