using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (задание на расчёт)")]
    [DesignationPlural("Технико-стоимостная проработка (задания на расчёт)")]
    public class PriceCalculationTask : BaseEntity
    {
        [Designation("Настройки"), Required]
        public virtual List<PriceCalculationTaskSetting> Settings { get; set; } = new List<PriceCalculationTaskSetting>();
    }
}