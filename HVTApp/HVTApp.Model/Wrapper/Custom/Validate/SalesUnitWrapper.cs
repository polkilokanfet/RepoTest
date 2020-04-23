using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesUnitWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if(Cost < SumPaid)
                yield return new ValidationResult("Сумма платежей превышает стоимость", new []{nameof(Cost), nameof(PaymentsActual)});
        }
    }
}