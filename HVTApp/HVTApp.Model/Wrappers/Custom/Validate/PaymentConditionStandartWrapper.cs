using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class PaymentConditionStandartWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Math.Abs((int) (PaymentsConditions.Sum(x => x.Part) - 1)) > 0.00001)
                yield return new ValidationResult("Сумма всех условий не равна 100%", new[] { nameof(PaymentsConditions) });
        }
    }
}
