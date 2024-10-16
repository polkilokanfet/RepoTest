using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public class UpdateStructureCostNumberTaskForConstructorViewModel : WrapperBase<UpdateStructureCostNumberTask>
    {
        public UpdateStructureCostNumberTaskForConstructorViewModel(UpdateStructureCostNumberTask model) : base(model) { }

        #region SimpleProperties

        public DateTime MomentStart
        {
            get => Model.MomentStart;
            set => SetValue(value);
        }
        public DateTime MomentStartOriginalValue => GetOriginalValue<DateTime>(nameof(MomentStart));
        public bool MomentStartIsChanged => GetIsChanged(nameof(MomentStart));

        public string StructureCostNumberOriginal
        {
            get => Model.StructureCostNumberOriginal;
            set => SetValue(value);
        }
        public string StructureCostNumberOriginalOriginalValue => GetOriginalValue<string>(nameof(StructureCostNumberOriginal));
        public bool StructureCostNumberOriginalIsChanged => GetIsChanged(nameof(StructureCostNumberOriginal));

        public string StructureCostNumber
        {
            get => Model.StructureCostNumber;
            set => SetValue(value);
        }
        public string StructureCostNumberOriginalValue => GetOriginalValue<string>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));

        #endregion

        #region ComplexProperties

	    public ProductBlockEmptyWrapper ProductBlock
        {
            get => GetWrapper<ProductBlockEmptyWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockEmptyWrapper>(ProductBlock, value);
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductBlockEmptyWrapper>(nameof(ProductBlock), Model.ProductBlock == null ? null : new ProductBlockEmptyWrapper(Model.ProductBlock));
        }

        #endregion
    }
}