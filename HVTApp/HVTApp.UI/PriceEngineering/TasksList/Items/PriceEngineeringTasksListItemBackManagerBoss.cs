using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTasksListItemBackManagerBoss : PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemBackManagerBoss>
    {
        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            return Entity.ChildPriceEngineeringTasks.SelectMany(task => task.SalesUnits);
        }

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

        public override bool ToShow => base.ToShow && Entity.BackManager == null;
    }
}