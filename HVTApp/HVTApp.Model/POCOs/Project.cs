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

        [Designation("Менеджер")]
        public virtual User Manager { get; set; }

        [Designation("Проектные единицы")]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("ТКП")]
        public virtual List<Offer> Offers { get; set; } = new List<Offer>();

        [Designation("Тендеры")]
        public virtual List<Tender> Tenders { get; set; } = new List<Tender>();

        [Designation("Заметки")]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }

}