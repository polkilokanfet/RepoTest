using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Страна
    /// </summary>
    [Designation("Страна")]
    public partial class Country : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(50)]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}