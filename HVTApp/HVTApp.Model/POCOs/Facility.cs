using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������ ��������.
    /// </summary>
    [Designation("������")]
    [DesignationPlural("�������")]
    public partial class Facility : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public virtual FacilityType Type { get; set; }

        [Designation("��������")]
        public virtual Company OwnerCompany { get; set; }

        [Designation("��������������")]
        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Type.ShortName})";
        }
    }
}