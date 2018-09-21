using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��� �������"), DesignationPlural("���� �������")]
    public partial class ProjectType : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(20)]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}