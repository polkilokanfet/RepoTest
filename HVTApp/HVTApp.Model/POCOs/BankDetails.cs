using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Банковские реквизиты")]
    [AllowEdit(Role.Economist, Role.SalesManager)]
    public partial class BankDetails : BaseEntity
    {
        [Designation("Банк"), Required, MaxLength(50), OrderStatus(5)]
        public string BankName { get; set; }

        [Designation("БИК"), Required, MaxLength(10), OrderStatus(4)]
        public string BankIdentificationCode { get; set; }

        [Designation("Кор.счет"), Required, MaxLength(50), OrderStatus(3)]
        public string CorrespondentAccount { get; set; }

        [Designation("Расч.счет"), Required, MaxLength(50), OrderStatus(2)]
        public string CheckingAccount { get; set; }
    }
}