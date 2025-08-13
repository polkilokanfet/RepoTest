using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Products.StructureCostsNumbers
{
    public class StructureCostsNumber : WrapperBase<ProductBlock>
    {
        public Product ProductKit { get; }
        public IEnumerable<DesignDepartment> DepartmentsKits { get; }

        public StructureCostsNumber(ProductBlock model) : base(model)
        {
        }

        public StructureCostsNumber(Product productKit, IEnumerable<DesignDepartment> departmentsKits) : base(productKit.ProductBlock)
        {
            ProductKit = productKit;
            DepartmentsKits = departmentsKits;
        }

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

        /// <summary>
        /// Требуется ли стракчакост
        /// </summary>
        public bool StructureCostNumberIsRequired
        {
            get => Model.StructureCostNumberIsRequired;
            set => SetValue(value);
        }
        public bool StructureCostNumberIsRequiredOriginalValue => GetOriginalValue<bool>(nameof(StructureCostNumberIsRequired));
        public bool StructureCostNumberIsRequiredIsChanged => GetIsChanged(nameof(StructureCostNumberIsRequired));

        public string ParametersString =>
            this.Model.Parameters
                .Select(parameter => $"[({parameter.ParameterGroup}) : ({parameter.Value})]")
                .OrderBy(x => x)
                .ToStringEnum();

    }
}