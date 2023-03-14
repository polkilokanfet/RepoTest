using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTasksListItemPlanMaker : 
        PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemPlanMaker>
    {
        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            //return Entity.ChildPriceEngineeringTasks
            //    .Where(x => x.GetSuitableTasksForWork(GlobalAppProperties.User).Any())
            //    .SelectMany(x => x.SalesUnits);
        }

        public PriceEngineeringTasksListItemPlanMaker(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemPlanMaker> GetChildTasks()
        {
            return Entity
                .GetSuitableTasksForOpenOrder(GlobalAppProperties.User)
                .Select(task => new PriceEngineeringTaskListItemPlanMaker(task));
        }
    }
}