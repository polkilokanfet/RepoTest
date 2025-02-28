using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProductIncludedWrapper1 : WrapperBase<ProductIncluded>
    {
        public ProductIncludedWrapper1(ProductIncluded model) : base(model) { }

        /// <summary>
        /// Количество
        /// </summary>
        public int Amount
        {
            get => Model.Amount;
            set => SetValue(value);
        }
        public int AmountOriginalValue => GetOriginalValue<int>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));
    }
}