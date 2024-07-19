using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class SpecificationLookup
    {
        [Designation("Контрагент"), OrderStatus(50)]
        public CompanyLookup Company => Contract?.Contragent;
    }
}