using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class CompanyWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (!ActivityFilds.Any())
                yield return new ValidationResult("У компании должна быть хотябы одна сфера деятельности.", new[] { nameof(ActivityFilds) });
        }
    }
}
