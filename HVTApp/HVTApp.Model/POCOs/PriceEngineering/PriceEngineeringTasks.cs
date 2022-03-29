using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (группа)")]
    [DesignationPlural("Технико-стоимостная проработка (группы)")]
    public class PriceEngineeringTasks : BaseEntity
    {
        [Designation("Менеджер"), Required, OrderStatus(1900)]
        public virtual User UserManager { get; set; }

        [Designation("Файлы технических требований (общие)"), OrderStatus(610)]
        public virtual List<PriceEngineeringTasksFileTechnicalRequirements> FilesTechnicalRequirements { get; set; } = new List<PriceEngineeringTasksFileTechnicalRequirements>();

        [Designation("Задачи"), OrderStatus(90)]
        public List<PriceEngineeringTask> ChildPriceEngineeringTasks { get; set; } = new List<PriceEngineeringTask>();
    }
}