using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class CompanyWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += OnParentCompanyChanged;
        }

        private void OnParentCompanyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != nameof(ParentCompany))
                return;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FullName))
                yield return new ValidationResult("FullName is required", new[] { nameof(FullName) });

            if (string.IsNullOrWhiteSpace(ShortName))
                yield return new ValidationResult("ShortName is required", new[] { nameof(ShortName) });

            if (Form == null)
                yield return new ValidationResult("Form is required", new[] { nameof(Form) });

        }
    }
}