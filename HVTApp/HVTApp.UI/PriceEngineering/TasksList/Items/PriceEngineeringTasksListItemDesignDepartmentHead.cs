using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTasksListItemDesignDepartmentHead : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemDesignDepartmentHead>
    {
        public override string Facilities =>
            Entity.ChildPriceEngineeringTasks
                .Where(x => x.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any())
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct()
                .OrderBy(x => x.Name)
                .ToStringEnum();

        public PriceEngineeringTasksListItemDesignDepartmentHead(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskListItemDesignDepartmentHead> GetChildTasks()
        {
            return Entity
                .GetSuitableTasksForInstruct(GlobalAppProperties.User)
                .Select(x => new PriceEngineeringTaskListItemDesignDepartmentHead(x));
        }
    }
}