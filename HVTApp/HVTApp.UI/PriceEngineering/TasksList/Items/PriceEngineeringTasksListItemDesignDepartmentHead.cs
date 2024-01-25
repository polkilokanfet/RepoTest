using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTasksListItemDesignDepartmentHead : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemDesignDepartmentHead>
    {
        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            return Entity.ChildPriceEngineeringTasks
                .Where(task => task.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any())
                .SelectMany(task => task.SalesUnits);
        }

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