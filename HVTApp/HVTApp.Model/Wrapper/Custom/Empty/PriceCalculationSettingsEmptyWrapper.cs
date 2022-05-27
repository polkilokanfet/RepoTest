using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper
{
    public class PriceCalculationSettingsEmptyWrapper : WrapperBase<PriceCalculationSettings>
    {
        public PriceCalculationSettingsEmptyWrapper(PriceCalculationSettings model) : base(model)
        {
        }
    }
}