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
            this.ComplexPropertyChanged += OnParentCompanyChanged;
        }

        private void OnParentCompanyChanged(object oldPropVal, object newPropVal, string propertyName)
        {
            if (propertyName != nameof(ParentCompany)) return;

            CompanyWrapper oldParent = oldPropVal as CompanyWrapper;
            if (oldParent != null && oldParent.ChildCompanies.Contains(this))
                oldParent.ChildCompanies.Remove(this);

            CompanyWrapper newParent = newPropVal as CompanyWrapper;
            if (newParent != null && !newParent.ChildCompanies.Contains(this))
                newParent.ChildCompanies.Add(this);
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