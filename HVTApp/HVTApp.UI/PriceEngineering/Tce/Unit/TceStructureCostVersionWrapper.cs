using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Model;
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

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                if (StructureCostVersion == null)
                {
                    yield return new ValidationResult("Version is required", new[] { nameof(StructureCostVersion) });
                }
                else if (StructureCostVersion.Value <= 0)
                {
                    yield return new ValidationResult("Version should be greater when 0", new[] { nameof(StructureCostVersion) });
                }
            }
        }
    }
}