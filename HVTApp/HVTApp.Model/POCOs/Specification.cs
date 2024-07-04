using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Спецификация")]
    public partial class Specification : BaseEntity
    {
        [Designation("№"), Required, MaxLength(10), OrderStatus(10)]
        public string Number { get; set; }

        [Designation("Дата"), Required, OrderStatus(9)]
        public DateTime Date { get; set; }

        [Designation("Дата подписания"), OrderStatus(5)]
        public DateTime? SignDate { get; set; }

        [Designation("НДС"), Required, OrderStatus(7)]
        public double Vat { get; set; } = GlobalAppProperties.Actual.Vat;

        [Designation("Договор"), Required, OrderStatus(8)]
        public virtual Contract Contract { get; set; }


        [Designation("Задачи ТСП")]
        public virtual List<PriceEngineeringTask> PriceEngineeringTasks { get; set; } = new List<PriceEngineeringTask>();

        [Designation("Задачи ТСЕ")]
        public virtual List<TechnicalRequrements> TechnicalRequrements { get; set; } = new List<TechnicalRequrements>();


        public override string ToString()
        {
            return Number;
        }
    }
}