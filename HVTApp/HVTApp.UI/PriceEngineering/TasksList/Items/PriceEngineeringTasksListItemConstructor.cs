using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTasksListItemConstructor : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemConstructor>
    {
        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            return Entity.ChildPriceEngineeringTasks
                .Where(x => x.GetSuitableTasksForWork(GlobalAppProperties.User).Any())
                .SelectMany(x => x.SalesUnits);
        }

        public PriceEngineeringTasksListItemConstructor(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemConstructor> GetChildTasks()
        {
            return Entity
                .GetSuitableTasksForWork(GlobalAppProperties.User)
                .Select(x => new PriceEngineeringTaskListItemConstructor(x));
        }
    }
}