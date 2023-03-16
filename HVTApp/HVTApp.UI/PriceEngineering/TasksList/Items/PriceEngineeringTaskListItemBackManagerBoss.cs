using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTaskListItemBackManagerBoss : PriceEngineeringTaskListItemBase
    {
        public PriceEngineeringTaskListItemBackManagerBoss(PriceEngineeringTask entity) : base(entity)
        {
        }

        public override bool ToShow
        {
            get
            {
                if (base.ToShow == false)
                    return false;

                if (this.Entity.Status.Equals(ScriptStep.ProductionRequestStart) &&
                    this.Entity.UserPlanMaker == null) return true;

                return false;
            }
        }
    }
}