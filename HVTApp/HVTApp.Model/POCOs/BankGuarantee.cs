using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("���������� ��������")]
    public class BankGuarantee : BaseEntity
    {
        [Designation("��� ��������"), Required]
        public virtual BankGuaranteeType BankGuaranteeType { get; set; }

        [Designation("�������"), Required]
        public double Percent { get; set; }

        [Designation("���� (����)"), Required]
        public int Days { get; set; } = 30;
    }

    [Designation("���������� �������� (���)")]
    public class BankGuaranteeType : BaseEntity
    {
        [Designation("��������"), Required]
        public string Name { get; set; }
    }
}