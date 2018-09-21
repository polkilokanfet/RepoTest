using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Единица измерения
    /// </summary>
    [Designation("Единица измерения")]
    public partial class Measure : BaseEntity
    {
        [Designation("Наименование"), Required, MaxLength(25)]
        public string FullName { get; set; }

        [Designation("Сокращенное наименование"), MaxLength(10)]
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, {ShortName}";
        }

    }
}