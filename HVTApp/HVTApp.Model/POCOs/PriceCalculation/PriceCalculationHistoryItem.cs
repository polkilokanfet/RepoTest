using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Элемент истории расчета ПЗ")]
    public class PriceCalculationHistoryItem : BaseEntity
    {
        [Designation("Id расчета ПЗ")]
        public Guid PriceCalculationId { get; set; }

        [Designation("Момент"), OrderStatus(50), Required]
        public DateTime Moment { get; set; } = DateTime.Now;

        [Designation("Тип элемента истории"), Required, OrderStatus(10)]
        public PriceCalculationHistoryItemType Type { get; set; } = PriceCalculationHistoryItemType.Start;

        [Designation("Комментарий"), OrderStatus(-10), MaxLength(500)]
        public string Comment { get; set; }

        [Designation("Автор"), OrderStatus(-15)]
        public virtual User User { get; set; }
    }

    public enum PriceCalculationHistoryItemType
    {
        Start,
        Stop,
        Reject,
        Finish
    }
}