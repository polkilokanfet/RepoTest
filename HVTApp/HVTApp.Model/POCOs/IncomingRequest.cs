using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Designation("��������"), OrderStatus(30), NotMapped]
        public bool IsDone => DoneDate.HasValue && DoneDate.Value <= DateTime.Now;

        [Designation("��������"), OrderStatus(20)]
        public bool IsActual { get; set; } = true;

        [Designation("���� ���������"), OrderStatus(35)]
        public DateTime? InstructionDate { get; set; }

        [Designation("���� ���������� ���������"), OrderStatus(25)]
        public DateTime? DoneDate { get; set; }
    }
}