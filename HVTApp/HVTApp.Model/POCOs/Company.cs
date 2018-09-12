using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Компания")]
    [DesignationPlural("Компании")]
    public partial class Company : BaseEntity
    {
        [Designation("Наименование")]
        public string FullName { get; set; }

        [Designation("Сокращенное наименование"), OrderStatus(10)]
        public string ShortName { get; set; }

        [Designation("ИНН")]
        public string Inn { get; set; }

        [Designation("КПП")]
        public string Kpp { get; set; }

        [Required, OrderStatus(5)]
        public virtual CompanyForm Form { get; set; }

        [Designation("Родительская компания")]
        public virtual Company ParentCompany { get; set; }


        [Designation("Юридический адрес")]
        public virtual Address AddressLegal { get; set; }

        [NotForListView]
        [Designation("Почтовый адрес")]
        public virtual Address AddressPost { get; set; }

        [Designation("Банковские реквизиты"), OrderStatus(-10)]
        public virtual List<BankDetails> BankDetailsList { get; set; } = new List<BankDetails>();

        [Designation("Сферы деятельности")]
        public virtual List<ActivityField> ActivityFilds { get; set; } = new List<ActivityField>();

        public override string ToString()
        {
            return $"{ShortName} ({Form?.ShortName})";
        }
    }
}
