using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    [DesignationPlural("�������")]
    public partial class Project : BaseEntity
    {
        [Designation("��������"), Required, OrderStatus(9), MaxLength(100)]
        public string Name { get; set; }

        [Designation("��� �������"), Required, OrderStatus(5)]
        public virtual ProjectType ProjectType { get; set; }

        [Designation("������� ����������� ��������"), OrderStatus(2)]
        public bool HighProbability { get; set; } = true;

        [Designation("�������� � ������"), OrderStatus(1)]
        public bool ForReport { get; set; } = true;

        [Designation("��������"), NotUpdate(Role.SalesManager), OrderStatus(4)]
        public virtual User Manager { get; set; }

        [Designation("�������"), OrderStatus(-10)]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}