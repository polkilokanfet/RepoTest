using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrappers
{
    public partial class FacilityWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("Не указано название объекта", new[] {nameof(Name)});

            if (Type == null)
                yield return new ValidationResult("Не указан тип объекта", new[] { nameof(Type) });

            if (OwnerCompany == null)
                yield return new ValidationResult("Не указан владелец объекта", new[] { nameof(OwnerCompany) });

        }
    }
}
