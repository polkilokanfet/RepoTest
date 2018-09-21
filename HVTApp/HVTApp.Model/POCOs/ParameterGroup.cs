using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Группа параметров")]
    public partial class ParameterGroup : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(50), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("Единица измерения")]
        public virtual Measure Measure { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}