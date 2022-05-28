using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public class PriceEngineeringTaskWrapper1 : WrapperBase<PriceEngineeringTask>
    {
        /// <summary>
        /// Настройки расчета ПЗ
        /// </summary>
        public PriceCalculationSettingsEmptyWrapper PriceCalculationSettings { get; set; }

        public PriceEngineeringTaskWrapper1(PriceEngineeringTask model) : base(model)
        {
            //InitializeComplexProperty(nameof(PriceCalculationSettings), Model.PriceCalculationTaskSettings == null ? null : new PriceCalculationSettingsEmptyWrapper(Model.PriceCalculationTaskSettings));
        }
    }
}