using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Компания")]
    [DesignationPlural("Компании")]
    [AllowEdit(Role.SalesManager, Role.DataBaseFiller, Role.Economist)]
    public partial class Company : BaseEntity
    {
        [Designation("Наименование"), Required, MaxLength(100), OrderStatus(20)]
        public string FullName { get; set; }

        [Designation("Сокращенное наименование"), Required, MaxLength(100), OrderStatus(15)]
        public string ShortName { get; set; }

        [Designation("email"), MaxLength(124)]
        public string Email { get; set; }

        [Designation("ИНН"), MaxLength(10)]
        public string Inn { get; set; }

        [Designation("КПП"), MaxLength(10)]
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

        [Designation("Сферы деятельности"), Required]
        public virtual List<ActivityField> ActivityFilds { get; set; } = new List<ActivityField>();

        /// <summary>
        /// Все головные компании.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Company> ParentCompanies()
        {
            if (ParentCompany == null)
                yield break;

            yield return ParentCompany;

            foreach (var parentCompany in ParentCompany.ParentCompanies())
            {
                yield return parentCompany;
            }
        }

        public override string ToString()
        {
            return $"{ShortName} ({Form?.ShortName})";
        }
    }
}
