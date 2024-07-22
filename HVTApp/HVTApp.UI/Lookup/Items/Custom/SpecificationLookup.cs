using System;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class SpecificationLookup : IComparable<SpecificationLookup>
    {
        [Designation("Контрагент"), OrderStatus(50)]
        public CompanyLookup Company => Contract?.Contragent;

        public int CompareTo(SpecificationLookup other)
        {
            if (this.Entity.Date > other.Entity.Date)
                return -1;

            if (Equals(this.Contract.Number, other.Contract.Number) == false)
                return string.Compare(this.Contract.Number, other.Contract.Number, StringComparison.Ordinal);

            if (Equals(this.Number, other.Number) == false)
                return string.Compare(this.Number, other.Number, StringComparison.Ordinal);

            return 0;
        }
    }
}