using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class SpecificationLookup
    {
        [Designation("����������"), OrderStatus(50)]
        public CompanyLookup Company => Contract?.Contragent;
    }
}