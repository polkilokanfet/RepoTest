using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Объект поставки.
    /// </summary>
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public virtual FacilityType Type { get; set; }
        public virtual Company OwnerCompany { get; set; }
        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Type.ShortName})";
        }
    }
}