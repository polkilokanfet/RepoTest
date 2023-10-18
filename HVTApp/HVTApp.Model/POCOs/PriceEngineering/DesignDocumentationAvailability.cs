using System;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Наличие КД
    /// </summary>
    [Designation("Наличие КД")]
    [DesignationPlural("Наличие КД")]
    public class DesignDocumentationAvailability
    {
        [Designation("Id задачи")]
        public virtual Guid? ParentPriceEngineeringTaskId { get; set; }

        [Designation("Требуется разработка КД")]
        public bool NeedDevelopment { get; set; }

        [Designation("Дней на разработку КД")]
        public short DaysToDevelopment { get; set; } = 0;
    }
}