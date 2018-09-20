using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class TenderWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if(!Types.Any())
                yield return new ValidationResult("Пустой список типов тендера", new []{nameof(Types)});
        }
    }
}