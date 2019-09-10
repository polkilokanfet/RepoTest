using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Объединение стран (СНГ и т.д.)
    /// </summary>
    [Designation("Объединение стран")]
    [DesignationPlural("Объединения стран")]
    public partial class CountryUnion : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(50), OrderStatus(2)]
        public string Name { get; set; }

        [Designation("Страны объединения"), Required, OrderStatus(1)]
        public virtual List<Country> Countries { get; set; } = new List<Country>();

        public override string ToString()
        {
            return Name;
        }
    }
}