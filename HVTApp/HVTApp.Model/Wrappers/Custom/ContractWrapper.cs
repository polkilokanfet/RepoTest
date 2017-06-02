using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ContractWrapper
    {
        public double Sum => Specifications.Sum(x => x.Sum);

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Number))
                yield return new ValidationResult("Не заполнен номер договора", new[] {nameof(Number)});

            if (Contragent == null)
                yield return new ValidationResult("Не указан контрагент", new[] {nameof(Contragent)});
        }
    }
}