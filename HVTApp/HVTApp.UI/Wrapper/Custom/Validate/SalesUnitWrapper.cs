using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.UI.Wrapper
{
    public partial class SalesUnitWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Cost < 0)
                yield return new ValidationResult("Стоимость не может быть отрицательной", new[] { nameof(Cost) });
        }
    }
}