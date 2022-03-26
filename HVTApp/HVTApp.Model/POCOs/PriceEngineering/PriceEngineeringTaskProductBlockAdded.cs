using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (добавленный блок)")]
    [DesignationPlural("Технико-стоимостная проработка (добавленные блоки)")]
    public class PriceEngineeringTaskProductBlockAdded : BaseEntity
    {
        [Designation("Id технико-стоимостной проработки"), Required, OrderStatus(500)]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("Количество"), Required, OrderStatus(950)]
        public int Amount { get; set; }

        [Designation("Блок продукта от менеджера"), Required, OrderStatus(900)]
        public virtual ProductBlock ProductBlock { get; set; }
    }
}