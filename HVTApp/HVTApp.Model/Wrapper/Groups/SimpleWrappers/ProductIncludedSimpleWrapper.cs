using System.ComponentModel;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper.Groups.SimpleWrappers
{
    public class ProductIncludedSimpleWrapper : WrapperBase<ProductIncluded>
    {
        #region SimpleProperties

        public double? CustomFixedPrice
        {
            get { return GetValue<double?>(); }
            set { SetValue(value); }
        }
        public double? CustomFixedPriceOriginalValue => GetOriginalValue<double?>(nameof(CustomFixedPrice));
        public bool CustomFixedPriceIsChanged => GetIsChanged(nameof(CustomFixedPrice));

        public int ParentsCount
        {
            get { return GetValue<System.Int32>(); }
            set { SetValue(value); }
        }
        public int ParentsCountOriginalValue => GetOriginalValue<int>(nameof(ParentsCount));
        public bool ParentsCountIsChanged => GetIsChanged(nameof(ParentsCount));

        public double Count => (double)Model.Amount / Model.ParentsCount;

        #endregion

        public ProductIncludedSimpleWrapper(ProductIncluded model) : base(model)
        {
        }

        public override void InitializeOther()
        {
            this.PropertyChanged += OnParentCountChanged;
        }

        private void OnParentCountChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ParentsCount)) return;
            OnPropertyChanged(nameof(Count));
        }

    }
}