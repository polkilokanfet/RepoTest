using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTasksListItemSalesManager : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemSalesManager>
    {
        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            return Entity.ChildPriceEngineeringTasks.SelectMany(task => task.SalesUnits);
        }

        public PriceEngineeringTasksListItemSalesManager(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemSalesManager> GetChildTasks()
        {
            return Entity
                .ChildPriceEngineeringTasks
                .SelectMany(task => task.GetAllPriceEngineeringTasks())
                .Select(task => new PriceEngineeringTaskListItemSalesManager(task));
        }
    }
}