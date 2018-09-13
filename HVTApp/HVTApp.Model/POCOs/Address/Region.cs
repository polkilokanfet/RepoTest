using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Область, край, республика и т.д.
    /// </summary>
    [Designation("Регион")]
    public partial class Region : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(50), OrderStatus(2)]
        public string Name { get; set; }

        [Designation("Округ"), Required, OrderStatus(1)]
        public virtual District District { get; set; }

        public override string ToString()
        {
            return $"{District}, {Name}";
        }
    }
}