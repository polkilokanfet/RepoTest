using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper
{
    public class PriceCalculationSettingsEmptyWrapper : WrapperBase<PriceCalculationTaskSetting>
    {
        public PriceCalculationSettingsEmptyWrapper(PriceCalculationTaskSetting model) : base(model)
        {
        }
    }
}