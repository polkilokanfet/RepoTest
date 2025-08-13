using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering
{
    public class ProductBlockStructureCostWrapperConstructor : ProductBlockStructureCostWrapper
    {
        #region StructureCostNumber

        public string StructureCostNumber
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string StructureCostNumberOriginalValue => GetOriginalValue<string>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));

        #endregion

        #region Design
        public string Design
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string DesignOriginalValue => GetOriginalValue<string>(nameof(Design));
        public bool DesignIsChanged => GetIsChanged(nameof(Design));

        #endregion

        public ProductBlockStructureCostWrapperConstructor(ProductBlock model) : base(model)
        {
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (Model.StructureCostNumberIsRequired && string.IsNullOrWhiteSpace(StructureCostNumber))
            {
                yield return new ValidationResult($"{nameof(StructureCostNumber)} is required", new[] { nameof(StructureCostNumber) });
            }
        }

        public override string PrintToMessage()
        {
            string info = string.Empty;
            if (StructureCostNumberIsChanged)
            {
                info = string.IsNullOrWhiteSpace(StructureCostNumberOriginalValue)
                    ? " (добавлен)"
                    : $" (изменен с {StructureCostNumberOriginalValue})";
            }

            return Model.StructureCostNumberIsRequired
                ? $"{Model} (SCC: {Model.StructureCostNumber}{info})"
                : $"{Model} (блок не требует состава (SCC){info})";
        }
    }
}