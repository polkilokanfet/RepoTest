using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTaskListItemPlanMaker : PriceEngineeringTaskListItemBase
    {
        public override bool ToShow => base.ToShow &&
                                       Entity.UserPlanMaker != null &&
                                       Entity.UserPlanMaker.Id == GlobalAppProperties.User.Id;

        public PriceEngineeringTaskListItemPlanMaker(PriceEngineeringTask entity) : base(entity)
        {
        }
    }
}