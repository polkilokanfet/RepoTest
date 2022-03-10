using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class StructureCostWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                yield return new ValidationResult("Номер стракчакоста не может быть пустым.", new[] { nameof(Number) });
            }

            if (AmountNumerator <= 0)
            {
                yield return new ValidationResult("Количество (числитель) должно быть положительным.", new[] { nameof(AmountNumerator) });
            }

            if (AmountDenomerator <= 0)
            {
                yield return new ValidationResult("Количество (знаменатель) должно быть положительным.", new[] { nameof(AmountDenomerator) });
            }
        }
    }
}