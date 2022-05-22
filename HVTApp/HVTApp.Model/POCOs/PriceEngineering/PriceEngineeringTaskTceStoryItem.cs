using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (TCE) - единица истории")]
    [DesignationPlural("Технико-стоимостные проработки (TCE) - единицы истории")]
    public class PriceEngineeringTaskTceStoryItem : BaseEntity
    {
        [Designation("Id задачи"), Required]
        public Guid PriceEngineeringTaskTceId { get; set; }

        [Designation("Момент"), Required]
        public DateTime Moment { get; set; } = DateTime.Now;

        [Designation("Действие"), Required]
        public PriceEngineeringTaskTceStoryItemStoryAction StoryAction { get; set; }
    }

    public enum PriceEngineeringTaskTceStoryItemStoryAction
    {
        Start, 
        Instruct,
        Finish
    }
}