using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тип проекта"), DesignationPlural("Типы проекта")]
    public partial class ProjectType : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(20)]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}