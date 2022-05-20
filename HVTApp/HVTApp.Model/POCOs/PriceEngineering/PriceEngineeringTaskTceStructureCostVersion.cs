using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (TCE) - версия стракчакоста")]
    [DesignationPlural("Технико-стоимостная проработка (TCE) - версии стракчакоста")]
    public class PriceEngineeringTaskTceStructureCostVersion : BaseEntity
    {
        [Designation("Id задачи"), Required]
        public Guid PriceEngineeringTaskTceId { get; set; }

        /// <summary>
        /// Id задачи или единицы добавленного блока
        /// </summary>
        [Designation("Id родительской сущности"), Required]
        public Guid ParentUnitId { get; set; }

        [Designation("Версия стракчакоста")]
        public int? StructureCostVersion { get; set; } = null;
    }
}