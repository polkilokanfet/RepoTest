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
        [Designation("����"), Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Designation("��������"), Required, MaxLength(50), OrderStatus(100)]
        public string Name { get; set; }

        [Designation("������� �������"), Required]
        public virtual List<BudgetUnit> Units { get; set; } = new List<BudgetUnit>();
    }
}