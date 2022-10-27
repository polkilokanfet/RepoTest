using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTasksListItemSalesManager : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemSalesManager>
    {
        public override string Facilities =>
            Entity.ChildPriceEngineeringTasks
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct()
                .OrderBy(x => x.Name)
                .ToStringEnum();

        public PriceEngineeringTasksListItemSalesManager(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemSalesManager> GetChildTasks()
        {
            return Entity
                .ChildPriceEngineeringTasks
                .SelectMany(x => x.GetAllPriceEngineeringTasks())
                .Select(x => new PriceEngineeringTaskListItemSalesManager(x));
        }
    }
}