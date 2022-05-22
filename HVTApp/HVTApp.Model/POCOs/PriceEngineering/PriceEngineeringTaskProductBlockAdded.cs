using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (добавленный блок)")]
    [DesignationPlural("Технико-стоимостная проработка (добавленные блоки)")]
    public class PriceEngineeringTaskProductBlockAdded : BaseEntity, IProductBlockContainer
    {
        [Designation("Id технико-стоимостной проработки"), Required, OrderStatus(500)]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("Количество"), Required, OrderStatus(950)]
        public int Amount { get; set; } = 1;

        [Designation("На каждый блок"), OrderStatus(800)]
        public bool IsOnBlock { get; set; } = true;

        [Designation("Блок продукта"), Required, OrderStatus(900)]
        public virtual ProductBlock ProductBlock { get; set; }

        public override string ToString()
        {
            string s = IsOnBlock ? "на каждый блок" : "на весь заказ";
            return $"{ProductBlock} = {Amount} шт. {s}, SCC: {ProductBlock.StructureCostNumber}";
        }
    }
}