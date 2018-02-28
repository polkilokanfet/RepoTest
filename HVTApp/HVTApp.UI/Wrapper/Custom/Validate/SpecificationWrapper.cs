using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class SpecificationWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Number))
                yield return new ValidationResult("Не указан номер спецификации", new[] {nameof(Number)});

            if (Contract == null)
                yield return new ValidationResult("Не указан контракт", new[] {nameof(Contract)});
        }
    }
}
