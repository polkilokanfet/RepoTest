using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Населенный пункт.
    /// </summary>
    [Designation("Населенный пункт")]
    [AllowEdit(Role.SalesManager)]
    public partial class Locality : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(50), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("Тип"), Required, OrderStatus(9)]
        public virtual LocalityType LocalityType { get; set; }

        [Designation("Регион"), Required, OrderStatus(8)]
        public virtual Region Region { get; set; }

        [Designation("Столица страны")]
        public bool IsCountryCapital { get; set; } = false;

        [Designation("Столица округа")]
        public bool IsDistrictCapital { get; set; } = false;

        [Designation("Столица региона")]
        public bool IsRegionCapital { get; set; } = false;

        [Designation("Расстояние до Екатеринбурга, км")]
        public double? DistanceToEkb { get; set; }

        public override string ToString()
        {
            return $"{LocalityType.ShortName} {Name}";
        }
    }
}