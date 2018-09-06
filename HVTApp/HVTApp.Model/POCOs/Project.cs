using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    [DesignationPlural("�������")]
    public class Project : BaseEntity
    {
        [Designation("��������"), OrderStatus(OrderStatus.High)]
        public string Name { get; set; }

        public virtual ProjectType ProjectType { get; set; }

        [Designation("��������"), NotUpdate(Role.SalesManager)]
        public virtual User Manager { get; set; }

        [Designation("�������"), OrderStatus(OrderStatus.Low)]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}