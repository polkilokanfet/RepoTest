using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class SpecificationWrapper
    {
        public double Sum => SalesUnits.Sum(x => x.CostTotal.Sum);

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Number))
                yield return new ValidationResult("Не указан номер спецификации", new[] {nameof(Number)});

            if (Contract == null)
                yield return new ValidationResult("Не указан контракт", new[] {nameof(Contract)});

            if (SalesUnits == null || SalesUnits.Count == 0)
                yield return new ValidationResult("У спецификации должна быть хотябы одна сбытовая единица", new[] {nameof(SalesUnits)});
        }
    }
}
