using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Адрес")]
    [DesignationPlural("Адреса")]
    [AllowEdit(Role.SalesManager)]
    public partial class Address : BaseEntity
    {
        [Designation("Описание"), Required, MaxLength(100)]
        public string Description { get; set; }

        [Designation("Населенный пункт"), Required, OrderStatus(5)]
        public virtual Locality Locality { get; set; }

        public override string ToString()
        {
            return Description;
        }

    }
}