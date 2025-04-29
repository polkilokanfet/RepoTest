using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTasksListItemObserver : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemObserver>
    {
        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            return Entity.ChildPriceEngineeringTasks
                .Where(task => task.GetSuitableTasksForObserve(GlobalAppProperties.User).Any())
                .SelectMany(task => task.SalesUnits);
        }

        public PriceEngineeringTasksListItemObserver(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemObserver> GetChildTasks()
        {
            return Entity
                .GetSuitableTasksForObserve(GlobalAppProperties.User)
                .Select(task => new PriceEngineeringTaskListItemObserver(task));
        }
    }
}