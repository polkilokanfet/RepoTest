using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Проект")]
    [DesignationPlural("Проекты")]
    public class Project : BaseEntity
    {
        [Designation("Название")]
        public string Name { get; set; }

        public ProjectType ProjectType { get; set; }

        [Designation("Менеджер"), NotUpdate(Role.SalesManager)]
        public virtual User Manager { get; set; }

        [Designation("Заметки")]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}