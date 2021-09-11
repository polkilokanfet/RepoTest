using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class AddressWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                yield return new ValidationResult("Описание не может быть пустым.", new[] { nameof(Description) });
            }
        }
    }
}