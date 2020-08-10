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

        [Designation("Дата (факт.)"), OrderStatus(40)]
        public DateTime? DateFinish { get; set; }

        [Designation("Дата (треб.)"), OrderStatus(35)]
        public DateTime? DateRequired { get; set; }

        [Designation("Заказ клиента"), OrderStatus(30), MaxLength(25)]
        public string ClientOrderNumber { get; set; }

        [Designation("Сервисный заказ"), OrderStatus(20), MaxLength(25)]
        public string ServiceOrderNumber { get; set; }
    }
}