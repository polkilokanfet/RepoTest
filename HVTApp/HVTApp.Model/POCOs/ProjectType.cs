using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��� �������"), DesignationPlural("���� �������")]
    public class ProjectType : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}