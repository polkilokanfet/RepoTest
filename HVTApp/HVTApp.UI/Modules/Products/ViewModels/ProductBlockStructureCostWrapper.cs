using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class ProductBlockStructureCostWrapper : WrapperBase<ProductBlock>
    {
        public string BlockName { get; }
        public string ProductType { get; }

        #region SimpleProperties

        public string StructureCostNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string StructureCostNumberOriginalValue => GetOriginalValue<string>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));

        public double Weight
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
        public double WeightOriginalValue => GetOriginalValue<double>(nameof(Weight));
        public bool WeightIsChanged => GetIsChanged(nameof(Weight));

        #endregion

        public ProductBlockStructureCostWrapper(ProductBlock model) : base(model)
        {
            BlockName = model.ToString();
            ProductType = model.ProductType.ToString();
        }
    }
}