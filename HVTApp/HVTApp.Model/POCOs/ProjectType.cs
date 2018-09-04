using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тип проекта"), DesignationPlural("Типы проекта")]
    public class ProjectType : BaseEntity
    {
        [Designation("Название")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}