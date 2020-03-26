using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("�������� ������")]
    public class IncomingRequest : BaseEntity
    {
        [Designation("������"), Required, OrderStatus(50)]
        public virtual Document Document { get; set; }

        [Designation("�����������"), OrderStatus(40)]
        public virtual List<Employee> Performers { get; set; } = new List<Employee>();

        [Designation("��������"), OrderStatus(30)]
        public bool IsDone { get; set; } = false;

        [Designation("��������"), OrderStatus(20)]
        public bool IsActual { get; set; } = true;
    }
}