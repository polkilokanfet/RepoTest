using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    public class Project : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        [Designation("��������")]
        public virtual User Manager { get; set; }

        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("�������")]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    public class ProjectUnit : BaseEntity
    {
        public Product Product { get; set; }
        public double Cost { get; set; }
    }
}