using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Проект")]
    public class Project : BaseEntity
    {
        [Designation("Название")]
        public string Name { get; set; }

        [Designation("Менеджер")]
        public virtual User Manager { get; set; }

        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("Заметки")]
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