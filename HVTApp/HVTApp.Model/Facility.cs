using HVTApp.Infrastructure;

namespace HVTApp.Model
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
            return $"{Name}, {Type.ShortName}";
        }
    }

    public class FacilityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }

}