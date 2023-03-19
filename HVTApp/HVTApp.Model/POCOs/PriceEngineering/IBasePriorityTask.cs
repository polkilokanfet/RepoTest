using System;
using System.Collections.Generic;

namespace HVTApp.Model.POCOs
{
    public interface IBasePriorityTask
    {
        /// <summary>
        /// Срок проработки (для выстраивания очередности задач)
        /// </summary>
        DateTime? TermPriority { get; set; }

        IEnumerable<PriceEngineeringTask> GetAllPriceEngineeringTasks();
    }
}