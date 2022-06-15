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
                //костыль для возбуждения валидации
                if (value)
                {
                    var original = StructureCostNumber;
                    StructureCostNumber = "1";
                    StructureCostNumber = original;
                }
            }
        }

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

        /// <summary>
        /// Чертеж
        /// </summary>
        public string Design
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string DesignOriginalValue => GetOriginalValue<string>(nameof(Design));
        public bool DesignIsChanged => GetIsChanged(nameof(Design));


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

        public string PrintToMessage()
        {
            string info = string.Empty;
            if (StructureCostNumberIsChanged)
            {
                info = string.IsNullOrWhiteSpace(StructureCostNumberOriginalValue)
                        ? " (добавлен)"
                        : $" (изменен с {StructureCostNumberOriginalValue})";
            }

            return $"{Model} (SCC: {StructureCostNumber}{info})";
        }
    }
}