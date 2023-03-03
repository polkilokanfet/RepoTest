using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.PriceEngineering.Items
{
    public abstract class PriceEngineeringTaskListItemBase : LookupItem<PriceEngineeringTask>
    {
        public virtual string StatusString => Entity?.Status.ToString();

        public virtual bool ToShow => Entity.Status.Show;

        protected PriceEngineeringTaskListItemBase(PriceEngineeringTask entity) : base(entity)
        {
        }
    }
}