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

        [Designation("�����������"), Required, OrderStatus(40)]
        public virtual List<Employee> Performers { get; set; } = new List<Employee>();

        [Designation("��������"), OrderStatus(30)]
        public bool IsDone { get; set; } = false;
    }
}