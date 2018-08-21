using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.UI.Lookup
{
    public partial class AddressLookup
    {
        [Designation("Регион")]
        public RegionLookup Region => this.Locality.Region;
        [Designation("Район")]
        public DistrictLookup District => this.Locality.Region.District;
        [Designation("Страна")]
        public CountryLookup Country => this.Locality.Region.District.Country;
    }
}