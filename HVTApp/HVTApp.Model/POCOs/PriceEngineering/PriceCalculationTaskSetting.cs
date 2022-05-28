using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (настройки калькуляции)")]
    [DesignationPlural("Технико-стоимостная проработка (настройки калькуляции)")]
    public class PriceCalculationTaskSetting : BaseEntity
    {
        [Required]
        public Guid PriceCalculationTaskId { get; set; }

        [Required]
        public Guid PriceEngineeringTaskId { get; set; }

        [Designation("Дата ОИТ"), Required]
        public DateTime DateOrderInTake { get; set; }

        [Designation("Дата реализации"), Required]
        public DateTime DateRealization { get; set; }

        [Designation("Условия оплаты"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }
    }
}