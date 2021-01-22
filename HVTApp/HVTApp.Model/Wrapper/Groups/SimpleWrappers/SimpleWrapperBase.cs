using HVTApp.Infrastructure;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper.Groups.SimpleWrappers
{
    public abstract class SimpleWrapperBase<TModel> : WrapperBase<TModel>
        where TModel : class, IBaseEntity
    {
        public override bool IsValid => true;
        public override bool IsChanged => false;

        protected SimpleWrapperBase(TModel model) : base(model)
        {
        }
    }
}