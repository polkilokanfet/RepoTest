using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrappers
{
    public partial class TestFriendAddressWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                yield return new ValidationResult("City is required", new[] { nameof(City) });
            }
        }
    }
}
