using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Банковские реквизиты")]
    public partial class BankDetails : BaseEntity
    {
        [Designation("Банк"), RequiredAttribute, MaxLength(50), OrderStatus(5)]
        public string BankName { get; set; }

        [Designation("БИК"), RequiredAttribute, MaxLength(10), OrderStatus(4)]
        public string BankIdentificationCode { get; set; }

        [Designation("Кор.счет"), RequiredAttribute, MaxLength(50), OrderStatus(3)]
        public string CorrespondentAccount { get; set; }

        [Designation("Расч.счет"), RequiredAttribute, MaxLength(50), OrderStatus(2)]
        public string CheckingAccount { get; set; }
    }
}