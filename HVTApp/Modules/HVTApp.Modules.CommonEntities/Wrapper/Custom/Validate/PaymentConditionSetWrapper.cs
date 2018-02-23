using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentConditionSetWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Math.Abs(PaymentConditions.Sum(x => x.Part) - 1) > 0.00001)
                yield return new ValidationResult("Сумма всех условий не равна 100%", new[] { nameof(PaymentConditions) });
        }
    }
}
