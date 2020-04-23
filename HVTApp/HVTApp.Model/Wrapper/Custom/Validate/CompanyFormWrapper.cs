using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class CompanyFormWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                yield return new ValidationResult("FullName is required", new[] { nameof(FullName) });
            }
        }
    }
}