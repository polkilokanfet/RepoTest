using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    [DesignationPlural("�������")]
    public partial class Project : BaseEntity
    {
        [Designation("��������"), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("��� �������")]
        public virtual ProjectType ProjectType { get; set; }

        [Designation("������� ����������� ��������")]
        public bool HighProbability { get; set; } = true;

        [Designation("��������"), NotUpdate(Role.SalesManager)]
        public virtual User Manager { get; set; }

        [Designation("�������"), OrderStatus(-10)]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}