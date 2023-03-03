using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTaskListItemConstructor : PriceEngineeringTaskListItemBase
    {
        public override bool ToShow => base.ToShow &&
                                       Entity.UserConstructor != null &&
                                       Entity.UserConstructor.Id == GlobalAppProperties.User.Id;

        public PriceEngineeringTaskListItemConstructor(PriceEngineeringTask entity) : base(entity)
        {
        }
    }
}