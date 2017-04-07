using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class PaymentConditionStandartWrapper
    {
        protected override void RunInConstructor()
        {
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Math.Abs(PaymentsConditions.Sum(x => x.PartInPercent) - 100) > 0.0001)
                yield return new ValidationResult("Сумма всех условий не равна 100%", new[] { nameof(PaymentsConditions) });
        }
    }
}
