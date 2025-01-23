using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesUnitWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.Model.PaymentsActual.Any())
            {
                if(Cost < this.Model.PaymentsActual.Sum(x => x.Sum))
                    yield return new ValidationResult("Сумма платежей превышает стоимость", new []{nameof(Cost), nameof(PaymentsActual)});
            }
        }
    }
}