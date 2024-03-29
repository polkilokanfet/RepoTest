using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Model.Wrapper
{
    public partial class StructureCostWrapper
    {
        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                yield return new ValidationResult("����� ������������ �� ����� ���� ������.", new[] { nameof(Number) });
            }

            if (AmountNumerator <= 0)
            {
                yield return new ValidationResult("���������� (���������) ������ ���� �������������.", new[] { nameof(AmountNumerator) });
            }

            if (AmountDenomerator <= 0)
            {
                yield return new ValidationResult("���������� (�����������) ������ ���� �������������.", new[] { nameof(AmountDenomerator) });
            }
        }
    }
}