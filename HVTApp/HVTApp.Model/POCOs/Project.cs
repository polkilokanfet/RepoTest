using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    [DesignationPlural("�������")]
    public class Project : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        public ProjectType ProjectType { get; set; }

        [Designation("��������"), NotUpdate(Role.SalesManager)]
        public virtual User Manager { get; set; }

        [Designation("�������")]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}