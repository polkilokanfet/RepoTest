using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������ ��������.
    /// </summary>
    [Designation("������")]
    public partial class Facility : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        public virtual FacilityType Type { get; set; }

        [Designation("��������")]
        public virtual Company OwnerCompany { get; set; }

        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Type.ShortName})";
        }
    }
}