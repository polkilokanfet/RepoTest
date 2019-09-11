using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class DocumentLookup
    {
        [Designation("Компания-отправитель"), OrderStatus(100)]
        public CompanyLookup CompanySender => this.SenderEmployee.Company;

        [Designation("Компания-получатель"), OrderStatus(99)]
        public CompanyLookup CompanyRecipient => this.RecipientEmployee.Company;
    }
}