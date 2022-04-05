using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.BlockChooser
{
    public class ProductBlockWrapper1 : ProductBlockWrapper
    {
        public ProductBlockWrapper1(ProductBlock model) : base(model)
        {
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(StructureCostNumber))
            {
                yield return new ValidationResult($"{nameof(StructureCostNumber)} is required", new[] { nameof(StructureCostNumber) });
            }
        }

    }
}