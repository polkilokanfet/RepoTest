using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class CompanyWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FullName))
                yield return new ValidationResult("FullName is required", new[] { nameof(FullName) });

            if (string.IsNullOrWhiteSpace(ShortName))
                yield return new ValidationResult("ShortName is required", new[] { nameof(ShortName) });

            if (Equals(FormId, default(Guid)))
                yield return new ValidationResult("Form is required", new[] { nameof(FormId) });

            if (!ActivityFilds.Any())
                yield return new ValidationResult("У компании должна быть хотябы одна сфера деятельности.", new[] { nameof(ActivityFilds) });
        }
    }
}
