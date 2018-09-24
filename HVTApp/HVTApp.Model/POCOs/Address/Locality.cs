using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ���������� �����.
    /// </summary>
    [Designation("���������� �����")]
    [AllowEdit(Role.SalesManager)]
    public partial class Locality : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(50), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("���"), Required, OrderStatus(9)]
        public virtual LocalityType LocalityType { get; set; }

        [Designation("������"), Required, OrderStatus(8)]
        public virtual Region Region { get; set; }

        [Designation("������� ������")]
        public bool IsCountryCapital { get; set; } = false;

        [Designation("������� ������")]
        public bool IsDistrictCapital { get; set; } = false;

        [Designation("������� �������")]
        public bool IsRegionCapital { get; set; } = false;

        [Designation("���������� �� �������������, ��")]
        public double? DistanceToEkb { get; set; }

        public override string ToString()
        {
            return $"{LocalityType.ShortName} {Name}";
        }
    }
}