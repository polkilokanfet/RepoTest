using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTasksListItemConstructor : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemConstructor>
    {
        public override string Facilities =>
            Entity.ChildPriceEngineeringTasks
                .Where(x => x.GetSuitableTasksForWork(GlobalAppProperties.User).Any())
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct()
                .OrderBy(x => x.Name)
                .ToStringEnum();

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