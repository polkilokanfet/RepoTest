using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.UI.Lookup
{
    public partial class AddressLookup
    {
        [Designation("������")]
        public RegionLookup Region => this.Locality.Region;
        [Designation("�����")]
        public DistrictLookup District => this.Locality.Region.District;
        [Designation("������")]
        public CountryLookup Country => this.Locality.Region.District.Country;
    }
}