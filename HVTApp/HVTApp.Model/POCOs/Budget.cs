using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Бюджет")]
    public class Budget : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(50), OrderStatus(100)]
        public string Name { get; set; }

        [Designation("Единицы бюджета"), Required]
        public virtual List<BudgetUnit> Units { get; set; } = new List<BudgetUnit>();
    }
}