using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class FacilityWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.Address == null)
            {
                if (OwnerCompany?.AddressLegal == null && OwnerCompany.Model.ParentCompanies().All(x => x.AddressLegal == null))
                {
                    yield return new ValidationResult("Не определено местоположение объекта.", new []{nameof(Address)});
                }
            }
        }
    }
}