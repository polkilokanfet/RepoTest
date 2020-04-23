using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class SpecificationWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrEmpty(Number))
                yield return new ValidationResult("Не указан номер спецификации", new[] {nameof(Number)});

            if (Contract == null)
                yield return new ValidationResult("Не указан контракт", new[] {nameof(Contract)});
        }
    }
}
