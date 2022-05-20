using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (TCE)")]
    [DesignationPlural("Технико-стоимостные проработки (TCE)")]
    public class PriceEngineeringTaskTce : BaseEntity
    {
        [Designation("Номер ТСЕ"), OrderStatus(2000), MaxLength(12)]
        public string TceNumber { get; set; }

        [Designation("Исполнитель"), OrderStatus(1800)]
        public virtual User BackManager { get; set; }

        [Designation("Связанные задачи верхнего уровня"), OrderStatus(900), Required]
        public virtual List<PriceEngineeringTask> PriceEngineeringTaskList { get; set; } = new List<PriceEngineeringTask>();

        [Designation("Версии стракчакостов"), OrderStatus(800), Required]
        public virtual List<PriceEngineeringTaskTceStructureCostVersion> SccVersions { get; set; } = new List<PriceEngineeringTaskTceStructureCostVersion>();

        [Designation("История проработки"), OrderStatus(700), Required]
        public virtual List<PriceEngineeringTaskTceStoryItem> StoryItems { get; set; } = new List<PriceEngineeringTaskTceStoryItem>();
    }
}