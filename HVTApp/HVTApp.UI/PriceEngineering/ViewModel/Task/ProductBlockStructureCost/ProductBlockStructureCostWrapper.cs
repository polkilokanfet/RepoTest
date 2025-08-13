using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class ProductBlockStructureCostWrapper : WrapperBase<ProductBlock>
    {

        protected ProductBlockStructureCostWrapper(ProductBlock model) : base(model)
        {
        }

        public virtual string PrintToMessage()
        {
            return Model.StructureCostNumberIsRequired
                ? $"{Model} (SCC: {Model.StructureCostNumber})"
                : $"{Model} (блок не требует состава (SCC))";
        }
    }
}