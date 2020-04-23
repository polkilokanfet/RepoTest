using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class OfferUnitWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (Cost < 0)
                yield return new ValidationResult("—тоимость не может быть отрицательной", new[] { nameof(Cost) });
        }
    }
}