using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Область рынка")]
    public class MarketField : BaseEntity
    {
        [Designation("Название"), Required, OrderStatus(10), MaxLength(30)]
        public string Name { get; set; }

        [Designation("Сферы деятельноси"), Required, OrderStatus(9)]
        public virtual List<ActivityField> ActivityFields { get; set; } = new List<ActivityField>();
    }
}