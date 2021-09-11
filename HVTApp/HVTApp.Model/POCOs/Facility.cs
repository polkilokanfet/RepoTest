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
    [AllowEdit(Role.SalesManager)]
    public partial class Facility : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(100), OrderStatus(20)]
        public string Name { get; set; }

        [Designation("���"), Required, OrderStatus(18)]
        public virtual FacilityType Type { get; set; }

        [Designation("��������"), Required, OrderStatus(16)]
        public virtual Company OwnerCompany { get; set; }

        [Designation("��������������")]
        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return Type == null 
            ? $"{Name}"
            : $"{Name} ({Type.ShortName})";
        }
    }
}