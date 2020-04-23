using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper.Test
{
    public partial class TestFriendWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                yield return new ValidationResult("Firstname is required",
                    new[] {nameof(FirstName)});
            }
            if (IsDeveloper && Emails.Count == 0)
            {
                yield return new ValidationResult("A developer must have an email-address",
                    new[] {nameof(IsDeveloper), nameof(Emails)});
            }
        }
    }
}
