using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    public class Budget : BaseEntity
    {
        [Designation("����"), Required, OrderStatus(110)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Designation("�����"), Required, OrderStatus(80)]
        public DateTime DateStart { get; set; }

        [Designation("�����"), Required, OrderStatus(70)]
        public DateTime DateFinish { get; set; }

        [Designation("��������"), Required, MaxLength(50), OrderStatus(100)]
        public string Name { get; set; }

        [Designation("������� �������"), Required]
        public virtual List<BudgetUnit> Units { get; set; } = new List<BudgetUnit>();

        public override string ToString()
        {
            return Name;
        }
    }
}