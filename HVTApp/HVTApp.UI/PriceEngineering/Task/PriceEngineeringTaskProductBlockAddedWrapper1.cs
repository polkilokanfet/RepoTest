using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskProductBlockAddedWrapper1 : WrapperBase<PriceEngineeringTaskProductBlockAdded>
    {
        public PriceEngineeringTaskProductBlockAddedWrapper1(PriceEngineeringTaskProductBlockAdded model) : base(model) { }

        #region SimpleProperties

        /// <summary>
        /// Количество
        /// </summary>
        public int Amount
        {
            get => GetValue<int>();
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }
        public int AmountOriginalValue => GetOriginalValue<int>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));

        /// <summary>
        /// На каждый блок
        /// </summary>
        public bool IsOnBlock
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool IsOnBlockOriginalValue => GetOriginalValue<bool>(nameof(IsOnBlock));
        public bool IsOnBlockIsChanged => GetIsChanged(nameof(IsOnBlock));
        #endregion

        #region ComplexProperties

        /// <summary>
        /// Блок продукта
        /// </summary>
        public ProductBlockStructureCostWrapper ProductBlock
        {
            get => GetWrapper<ProductBlockStructureCostWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockStructureCostWrapper>(ProductBlock, value);
        }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(ProductBlock), Model.ProductBlock == null ? null : new ProductBlockStructureCostWrapper(Model.ProductBlock, true));
        }
    }
}