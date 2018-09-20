using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendEmailWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult("Email is required", new[] {nameof(Email)});
            }
            if (!new EmailAddressAttribute().IsValid(Email))
            {
                yield return new ValidationResult("Email is not a valid email address", new[] {nameof(Email)});
            }
        }
    }
}
