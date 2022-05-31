using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper
{
    public class PriceCalculationEmptyWrapper : WrapperBase<PriceCalculation>
    {
        public PriceCalculationEmptyWrapper(PriceCalculation model) : base(model)
        {
        }
    }
}