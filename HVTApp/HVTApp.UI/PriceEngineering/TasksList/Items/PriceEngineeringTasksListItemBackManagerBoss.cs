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
                .SelectMany(priceEngineeringTask => priceEngineeringTask.GetAllPriceEngineeringTasks())
                .Select(priceEngineeringTask => new PriceEngineeringTaskListItemBackManagerBoss(priceEngineeringTask));
        }

        public override bool ToShow
        {
            get
            {
                if (base.ToShow)
                    return true;

                if (Entity.ChildPriceEngineeringTasks.Any(task => task.Status.Equals(ScriptStep.ProductionRequestStop)))
                    return true;

                return Entity.BackManager == null &&
                       Entity.ChildPriceEngineeringTasks.Any(task => task.Status.Equals(ScriptStep.LoadToTceStart));
            }
        }
    }
}