using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.PriceMaking.LaborCosts
{
    public class LaborCostsWrapper : WrapperBase<ProductBlock>
    {
        /// <summary>
        /// Трудозатраты (н/ч на ед.)
        /// </summary>
        public double? LaborCosts
        {
            get => GetValue<double?>();
            set => SetValue(value);
        }
        public double? LaborCostsOriginalValue => GetOriginalValue<double?>(nameof(LaborCosts));
        public bool LaborCostsIsChanged => GetIsChanged(nameof(LaborCosts));

        public LaborCostsWrapper(ProductBlock model) : base(model)
        {
        }
    }
}
