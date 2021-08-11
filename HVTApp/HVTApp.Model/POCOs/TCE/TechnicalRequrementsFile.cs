using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("���.������� (����)")]
    [DesignationPlural("����� ��")]
    public partial class TechnicalRequrementsFile : BaseEntity
    {
        [Designation("����"), OrderStatus(300)]
        public virtual DateTime? Date { get; set; } = DateTime.Now;

        [Designation("���"), Required, MaxLength(50), OrderStatus(20)]
        public string Name { get; set; }

        [Designation("�����������"), MaxLength(250), OrderStatus(10)]
        public string Comment { get; set; }

        [Designation("���������"), OrderStatus(2)]
        public bool IsActual { get; set; } = true;

        public override string ToString()
        {
            return Name;
        }
    }
}