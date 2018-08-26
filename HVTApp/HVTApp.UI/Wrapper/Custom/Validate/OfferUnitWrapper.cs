using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Cost < 0)
                yield return new ValidationResult("—тоимость не может быть отрицательной", new[] { nameof(Cost) });
        }
    }
}