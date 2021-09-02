using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class PaymentConditionWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (Part <= 0.0)
                yield return new ValidationResult("Процент должен быть больше 0", new[] { nameof(Part) });

            if (Part > 1.0)
                yield return new ValidationResult("Процент не должен быть больше 100%", new[] { nameof(Part) });
        }
    }
}