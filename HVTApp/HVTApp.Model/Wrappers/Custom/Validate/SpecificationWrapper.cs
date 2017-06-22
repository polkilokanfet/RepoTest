using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class SpecificationWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Number))
                yield return new ValidationResult("Не указан номер спецификации", new[] {nameof(Number)});

            if (Contract == null)
                yield return new ValidationResult("Не указан контракт", new[] {nameof(Contract)});

            if (ProductComplexUnits == null || ProductComplexUnits.Count == 0)
                yield return new ValidationResult("У спецификации должна быть хотябы одна сбытовая единица", new[] {nameof(ProductComplexUnits)});
        }
    }
}
