using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering
{
    public class ProductBlockStructureCostWrapper : WrapperBase<ProductBlock>
    {
        public bool ValidateStructureCostNumber { get; set; }

        /// <summary>
        /// Сралчахвост
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