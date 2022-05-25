using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (настройки калькуляции)")]
    [DesignationPlural("Технико-стоимостная проработка (настройки калькуляции)")]
    public class PriceCalculationSettings : BaseEntity
    {
        [Designation("Id ТСП"), Required]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        /// <summary>
        /// Момент старта задачи ТСЕ
        /// </summary>
        [Designation("Момент старта задачи ТСЕ"), Required]
        public DateTime StartMoment { get; set; }

        [Designation("Дата ОИТ"), Required]
        public DateTime DateOrderInTake { get; set; }

        [Designation("Дата реализации"), Required]
        public DateTime DateRealization { get; set; }

        [Designation("Условия оплаты"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }
    }
}