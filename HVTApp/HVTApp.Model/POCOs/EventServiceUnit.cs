using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("EventServiceUnit")]
    public partial class EventServiceUnit : BaseEntity
    {
        [Designation("Пользователь"), Required, OrderStatus(10)]
        public virtual User User { get; set; }

        [Designation("Id целевой сущности"), Required, OrderStatus(5)]
        public virtual Guid TargetEntityId { get; set; }

        [Designation("Тип действия"), Required, OrderStatus(7)]
        public EventServiceActionType EventServiceActionType { get; set; }
    }

    public enum EventServiceActionType
    {
        StartPriceCalculation
    }
}