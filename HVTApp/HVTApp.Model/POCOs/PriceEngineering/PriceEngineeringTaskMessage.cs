using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (сообщение)")]
    [DesignationPlural("Технико-стоимостная проработка (сообщения)")]
    public class PriceEngineeringTaskMessage : BaseEntity, IMessage
    {
        [Designation("Id технико-стоимостной проработки"), Required, OrderStatus(900)]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("Автор"), Required, OrderStatus(800)]
        public virtual User Author { get; set; }

        [Designation("Момент"), Required, OrderStatus(700)]
        public virtual DateTime Moment { get; set; } = DateTime.Now;

        [Designation("Сообщение"), Required, OrderStatus(100)]
        public string Message { get; set; }
    }

    public interface IMessage
    {
        DateTime Moment { get; } 
        string Message { get; }
    }
}