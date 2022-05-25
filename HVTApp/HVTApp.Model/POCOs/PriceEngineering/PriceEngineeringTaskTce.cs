using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        [Designation("Расчеты ПЗ"), OrderStatus(600)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();

        [NotMapped]
        public PriceEngineeringTaskTceStoryItemStoryAction LastAction => StoryItems.OrderBy(x => x.Moment).Last().StoryAction;

        [Designation("Менеджер"), NotMapped]
        public User FrontManager =>
            this.PriceEngineeringTaskList.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager;

        [Designation("Старт"), NotMapped]
        public DateTime? StartMoment =>
            this.StoryItems
                .Where(x => x.StoryAction == PriceEngineeringTaskTceStoryItemStoryAction.Start)
                .OrderBy(x => x.Moment)
                .LastOrDefault()?.Moment;

        [Designation("Финиш"), NotMapped]
        public DateTime? FinishMoment =>
            this.StoryItems
                .Where(x => x.StoryAction == PriceEngineeringTaskTceStoryItemStoryAction.Finish)
                .OrderBy(x => x.Moment)
                .LastOrDefault()?.Moment;
    }
}