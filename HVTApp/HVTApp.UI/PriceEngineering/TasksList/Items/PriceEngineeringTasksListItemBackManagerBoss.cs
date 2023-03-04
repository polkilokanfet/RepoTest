using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTasksListItemBackManagerBoss : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemBackManagerBoss>
    {
        public override string Facilities =>
            Entity.ChildPriceEngineeringTasks
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct()
                .OrderBy(x => x.Name)
                .ToStringEnum();

        public PriceEngineeringTasksListItemBackManagerBoss(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemBackManagerBoss> GetChildTasks()
        {
            return Entity
                .ChildPriceEngineeringTasks
                .SelectMany(x => x.GetAllPriceEngineeringTasks())
                .Select(x => new PriceEngineeringTaskListItemBackManagerBoss(x));
        }
    }
}