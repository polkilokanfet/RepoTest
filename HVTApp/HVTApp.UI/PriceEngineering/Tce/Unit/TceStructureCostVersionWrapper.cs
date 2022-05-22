using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public class TceStructureCostVersionWrapper : WrapperBase<PriceEngineeringTaskTceStructureCostVersion>
    {
        /// <summary>
        /// Версия стракчакоста
        /// </summary>
        public int? StructureCostVersion
        {
            get => Model.StructureCostVersion;
            set => SetValue(value);
        }
        public int? StructureCostVersionOriginalValue => GetOriginalValue<int?>(nameof(StructureCostVersion));
        public bool StructureCostVersionIsChanged => GetIsChanged(nameof(StructureCostVersion));

        public TceStructureCostVersionWrapper(PriceEngineeringTaskTceStructureCostVersion model) : base(model)
        {
        }
    }
}