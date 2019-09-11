using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class DocumentLookup
    {
        [Designation("��������-�����������"), OrderStatus(100)]
        public CompanyLookup CompanySender => this.SenderEmployee.Company;

        [Designation("��������-����������"), OrderStatus(99)]
        public CompanyLookup CompanyRecipient => this.RecipientEmployee.Company;
    }
}