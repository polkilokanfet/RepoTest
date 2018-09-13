using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.UI.Wrapper
{
    public partial class SalesUnitWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var result in base.Validate(validationContext))
            {
                yield return result;
            }

            if(Cost < SumPaid)
                yield return new ValidationResult("Сумма платежей превышает стоимость", new []{nameof(Cost), nameof(PaymentsActual)});
        }
    }
}