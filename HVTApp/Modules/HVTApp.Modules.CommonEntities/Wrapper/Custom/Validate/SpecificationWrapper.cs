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

            if (ContractId == Guid.Empty)
                yield return new ValidationResult("Не указан контракт", new[] {nameof(ContractId)});

            if (SalesUnits == null || SalesUnits.Count == 0)
                yield return new ValidationResult("У спецификации должна быть хотябы одна сбытовая единица", new[] {nameof(SalesUnits)});
        }
    }
}
