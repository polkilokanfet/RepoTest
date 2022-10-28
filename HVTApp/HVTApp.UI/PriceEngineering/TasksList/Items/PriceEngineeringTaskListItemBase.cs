using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.PriceEngineering.Items
{
    public abstract class PriceEngineeringTaskListItemBase : LookupItem<PriceEngineeringTask>
    {
        public virtual string StatusString => Entity?.Status.StatusToString();

        public virtual bool ToShow => Entity.Status != PriceEngineeringTaskStatusEnum.Stopped &&
                                      Entity.Status != PriceEngineeringTaskStatusEnum.Accepted;

        protected PriceEngineeringTaskListItemBase(PriceEngineeringTask entity) : base(entity)
        {
        }
    }
}