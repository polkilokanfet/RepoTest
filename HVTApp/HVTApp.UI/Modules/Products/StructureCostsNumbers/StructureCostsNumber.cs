using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Products.StructureCostsNumbers
{
    public class StructureCostsNumber : WrapperBase<ProductBlock>
    {
        public StructureCostsNumber(ProductBlock model) : base(model) { }

        /// <summary>
        /// Сралчахвост
        /// </summary>
        public string StructureCostNumber
        {
            get => Model.StructureCostNumber;
            set => SetValue(value);
        }
        public string StructureCostNumberOriginalValue => GetOriginalValue<string>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));

        /// <summary>
        /// Чертеж
        /// </summary>
        public string Design
        {
            get => Model.Design;
            set => SetValue(value);
        }
        public string DesignOriginalValue => GetOriginalValue<string>(nameof(Design));
        public bool DesignIsChanged => GetIsChanged(nameof(Design));

        public string ParametersString =>
            this.Model.Parameters
                .Select(parameter => $"[({parameter.ParameterGroup}) : ({parameter.Value})]")
                .OrderBy(x => x)
                .ToStringEnum();

    }
}