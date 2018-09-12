using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ���������� �����.
    /// </summary>
    [Designation("���������� �����")]
    public partial class Locality : BaseEntity
    {
        public string Name { get; set; }
        public virtual LocalityType LocalityType { get; set; }
        public virtual Region Region { get; set; }

        public bool IsCountryCapital { get; set; } = false;
        public bool IsDistrictCapital { get; set; } = false;
        public bool IsRegionCapital { get; set; } = false;

        public double? StandartDeliveryPeriod { get; set; }

        [Designation("���������� �� �������������")]
        public double? DistanceToEkb { get; set; }

        public override string ToString()
        {
            return $"{LocalityType.ShortName} {Name}";
        }
    }
}