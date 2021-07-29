using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class OrderWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                yield return new ValidationResult("Номер з/з не может быть пустым", new[] {nameof(Number)});
            }
        }
    }
}