using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.UI.Wrapper.Test
{
    public partial class TestFriendAddressWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                yield return new ValidationResult("City is required", new[] { nameof(City) });
            }
        }
    }
}
