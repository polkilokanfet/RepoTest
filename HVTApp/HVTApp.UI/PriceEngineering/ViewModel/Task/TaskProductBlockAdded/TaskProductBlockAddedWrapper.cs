using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskProductBlockAddedWrapper : WrapperBase<PriceEngineeringTaskProductBlockAdded>
    {
        protected TaskProductBlockAddedWrapper(PriceEngineeringTaskProductBlockAdded model) : base(model)
        {
        }
    }
}