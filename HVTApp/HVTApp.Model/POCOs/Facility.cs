using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Объект поставки.
    /// </summary>
    [Designation("Объект")]
    [DesignationPlural("Объекты")]
    public partial class Facility : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public virtual FacilityType Type { get; set; }

        [Designation("Владелец")]
        public virtual Company OwnerCompany { get; set; }

        [Designation("Местоположение")]
        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Type.ShortName})";
        }
    }
}