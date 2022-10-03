using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class SccVersionWrapper : StructureCostVersionWrapper
    {
        /// <summary>
        /// Этот scc является целевым для задачи (важно для валидации объекта)
        /// </summary>
        public bool IsTarget { get; set; } = false;

        public string Name { get; }

        public SccVersionWrapper(StructureCostVersion model, string name) : base(model)
        {
            Name = name;
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager && IsTarget)
            {
                if(Version.HasValue == false)
                    yield return new ValidationResult("Version is required", new[] { nameof(Version) });
                else if (Version.Value < 1)
                    yield return new ValidationResult("Version should be greater then 0", new[] { nameof(Version) });
                else if (Version.Value > 99)
                    yield return new ValidationResult("Version should be less then 99", new[] { nameof(Version) });
            }
        }
    }
}