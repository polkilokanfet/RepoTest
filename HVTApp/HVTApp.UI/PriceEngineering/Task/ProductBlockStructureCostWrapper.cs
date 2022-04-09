using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering
{
    public class ProductBlockStructureCostWrapper : WrapperBase<ProductBlock>
    {
        private bool _validateStructureCostNumber;

        private bool ValidateStructureCostNumber
        {
            get => _validateStructureCostNumber;
            set
            {
                _validateStructureCostNumber = value;
                //костыль дл€ возбуждени€ валидации
                if (value)
                {
                    var original = StructureCostNumber;
                    StructureCostNumber = "1";
                    StructureCostNumber = original;
                }
            }
        }

        /// <summary>
        /// —ралчахвост
        /// </summary>
        public string StructureCostNumber
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string StructureCostNumberOriginalValue => GetOriginalValue<string>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));

        public ProductBlockStructureCostWrapper(ProductBlock model, bool validateStructureCostNumber = false) : base(model)
        {
            ValidateStructureCostNumber = validateStructureCostNumber;
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (ValidateStructureCostNumber)
            {
                if (string.IsNullOrWhiteSpace(StructureCostNumber))
                {
                    yield return new ValidationResult($"{nameof(StructureCostNumber)} is required", new[] { nameof(StructureCostNumber) });
                }
            }
        }
    }
}