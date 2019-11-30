using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    public class PriceCalculation : BaseEntity
    {
        [Designation("Автор задачи"), Required]
        public User Author { get; set; }

        [Designation("Старт задачи"), Required]
        public DateTime TaskOpenMoment { get; set; }

        [Designation("Финиш задачи")]
        public DateTime? TaskCloseMoment { get; set; }

        [Designation("Комментарий"), MaxLength(200)]
        public string Comment { get; set; }

        [Designation("Единицы продаж"), Required]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();
    }
}