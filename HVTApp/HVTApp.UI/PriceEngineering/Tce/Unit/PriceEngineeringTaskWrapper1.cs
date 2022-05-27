using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public class PriceEngineeringTaskWrapper1 : WrapperBase<PriceEngineeringTask>
    {
        /// <summary>
        /// Настройки расчета ПЗ
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceCalculationSettingsEmptyWrapper> PriceCalculationSettingsList { get; }

        public PriceEngineeringTaskWrapper1(PriceEngineeringTask model) : base(model)
        {
            if (Model.PriceCalculationSettingsList == null) throw new ArgumentException("PriceCalculationSettingsList cannot be null");
            PriceCalculationSettingsList = new ValidatableChangeTrackingCollection<PriceCalculationSettingsEmptyWrapper>(Model.PriceCalculationSettingsList.Select(e => new PriceCalculationSettingsEmptyWrapper(e)));
            RegisterCollection(PriceCalculationSettingsList, Model.PriceCalculationSettingsList);
        }
    }
}