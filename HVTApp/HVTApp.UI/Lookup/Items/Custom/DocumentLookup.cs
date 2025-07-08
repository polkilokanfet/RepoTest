using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class DocumentLookup
    {
        [Designation("Компания-отправитель"), OrderStatus(100)]
        public string CompanySender => this.SenderEmployee?.Company.ToString();

        [Designation("Компания-получатель"), OrderStatus(99)]
        public string CompanyRecipient => this.RecipientEmployee?.Company.ToString();

        public List<Employee> Performers { get; } = new List<Employee>();

        public DocumentLookup(Document document, List<Employee> performers) : base(document)
        {
            if (performers != null)
                Performers = performers.ToList();
        }
    }
}