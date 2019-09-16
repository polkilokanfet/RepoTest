using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Банковская гарантия")]
    public class BankGuarantee : BaseEntity
    {
        [Designation("Тип гарантии"), Required]
        public virtual BankGuaranteeType BankGuaranteeType { get; set; }

        [Designation("Процент"), Required]
        public double Percent { get; set; }

        [Designation("Срок (дней)"), Required]
        public int Days { get; set; } = 30;
    }

    [Designation("Банковская гарантия (тип)")]
    public class BankGuaranteeType : BaseEntity
    {
        [Designation("Название"), Required]
        public string Name { get; set; }
    }
}