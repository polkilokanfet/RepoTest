using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Объект поставки.
    /// </summary>
    [Designation("Объект")]
    public partial class Facility : BaseEntity
    {
        [Designation("Название")]
        public string Name { get; set; }

        public virtual FacilityType Type { get; set; }

        [Designation("Владелец")]
        public virtual Company OwnerCompany { get; set; }

        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Type.ShortName})";
        }
    }
}