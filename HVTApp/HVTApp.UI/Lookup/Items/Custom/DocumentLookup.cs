using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class DocumentLookup
    {
        [Designation("Компания-отправитель"), OrderStatus(100)]
        public CompanyLookup CompanySender => this.SenderEmployee.Company;

        [Designation("Компания-получатель"), OrderStatus(99)]
        public CompanyLookup CompanyRecipient => this.RecipientEmployee.Company;

        public List<Employee> Performers { get; } = new List<Employee>();

        public DocumentLookup(Document document, List<Employee> performers) : base(document)
        {
            if (performers != null)
                Performers = performers.ToList();
        }
    }
}