using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Шеф-монтаж")]
    public class Supervision : BaseEntity
    {
        [Designation("Оборудование"), Required, OrderStatus(50)]
        public virtual SalesUnit SalesUnit { get; set; }

        /// <summary>
        /// Это если шеф-монтаж не входит в стоимость оборудования, а выделен отдельной строкой.
        /// </summary>
        [Designation("Единица шеф-монтажа"), OrderStatus(45)]
        public virtual SalesUnit SupervisionUnit { get; set; }

        [Designation("Дата"), OrderStatus(40)]
        public DateTime? DateFinish { get; set; }

        [Designation("Сервисный заказ"), OrderStatus(30), MaxLength(25)]
        public string ServiceOrderNumber { get; set; }
    }
}