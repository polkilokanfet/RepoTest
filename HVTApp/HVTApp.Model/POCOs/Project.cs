using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Проект")]
    [DesignationPlural("Проекты")]
    public partial class Project : BaseEntity
    {
        [Designation("Название"), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("Тип проекта")]
        public virtual ProjectType ProjectType { get; set; }

        [Designation("Высокая вероятность поставки")]
        public bool HighProbability { get; set; } = true;

        [Designation("Менеджер"), NotUpdate(Role.SalesManager)]
        public virtual User Manager { get; set; }

        [Designation("Заметки"), OrderStatus(-10)]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}