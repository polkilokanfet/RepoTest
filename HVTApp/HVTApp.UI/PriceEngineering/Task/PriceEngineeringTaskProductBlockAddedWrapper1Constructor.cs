using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskProductBlockAddedWrapper1Constructor : PriceEngineeringTaskProductBlockAddedWrapper1
    {
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

        /// <summary>
        /// Удалено
        /// </summary>
        public bool IsRemoved
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool IsRemovedOriginalValue => GetOriginalValue<bool>(nameof(IsRemoved));
        public bool IsRemovedIsChanged => GetIsChanged(nameof(IsRemoved));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Блок продукта
        /// </summary>
        public ProductBlockStructureCostWrapperConstructor ProductBlock
        {
            get => GetWrapper<ProductBlockStructureCostWrapperConstructor>();
            set => SetComplexValue<ProductBlock, ProductBlockStructureCostWrapperConstructor>(ProductBlock, value);
        }

        #endregion

        public PriceEngineeringTaskProductBlockAddedWrapper1Constructor(PriceEngineeringTaskProductBlockAdded model) : base(model)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(ProductBlock), Model.ProductBlock == null ? null : new ProductBlockStructureCostWrapperConstructor(Model.ProductBlock));
        }

        public override string ToString()
        {
            if (ProductBlock.StructureCostNumberIsChanged == false) 
                return Model.ToString();

            string s = string.IsNullOrWhiteSpace(ProductBlock.StructureCostNumberOriginalValue) 
                ? "добавлен" 
                : $"изменен с {ProductBlock.StructureCostNumberOriginalValue}";
                
            return $"{Model} ({s})";
        }
    }
}