using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class PaymentConditionSetWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (Math.Abs(PaymentConditions.Sum(paymentCondition => paymentCondition.Part) - 1) > 0.00001)
                yield return new ValidationResult("Сумма всех условий не равна 100%", new[] { nameof(PaymentConditions) });
        }
    }
}
