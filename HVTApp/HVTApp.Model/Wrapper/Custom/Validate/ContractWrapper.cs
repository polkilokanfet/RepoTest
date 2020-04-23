using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ContractWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrEmpty(Number))
                yield return new ValidationResult("�� �������� ����� ��������", new[] {nameof(Number)});

            if (Contragent == null)
                yield return new ValidationResult("�� ������ ����������", new[] {nameof(Contragent)});
        }
    }
}