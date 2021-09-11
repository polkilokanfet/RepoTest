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
    [AllowEdit(Role.SalesManager)]
    public partial class Facility : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(100), OrderStatus(20)]
        public string Name { get; set; }

        [Designation("Тип"), Required, OrderStatus(18)]
        public virtual FacilityType Type { get; set; }

        [Designation("Владелец"), Required, OrderStatus(16)]
        public virtual Company OwnerCompany { get; set; }

        [Designation("Местоположение")]
        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return Type == null 
            ? $"{Name}"
            : $"{Name} ({Type.ShortName})";
        }
    }
}