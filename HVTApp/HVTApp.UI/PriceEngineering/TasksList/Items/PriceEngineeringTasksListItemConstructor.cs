using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTasksListItemConstructor : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemConstructor>
    {
        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            return Entity.ChildPriceEngineeringTasks
                .Where(task => task.GetSuitableTasksForWork(GlobalAppProperties.User).Any() ||
                               task.GetSuitableTasksForInspect(GlobalAppProperties.User).Any())
                .SelectMany(task => task.SalesUnits);
        }

        public PriceEngineeringTasksListItemConstructor(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemConstructor> GetChildTasks()
        {
            return Entity
                .GetSuitableTasksForWork(GlobalAppProperties.User)
                .Union(Entity.GetSuitableTasksForInspect(GlobalAppProperties.User))
                .Select(task => new PriceEngineeringTaskListItemConstructor(task));
        }
    }
}