using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ProductParameterWrapper
    {
        public bool CanBeSelected(IEnumerable<ProductParameterWrapper> parameters)
        {
            if (!ProductParameterSets.Any()) return true;

            foreach (var parameterSet in ProductParameterSets)
                if (parameterSet.RequiredParentParameters.All(parameters.Contains)) return true;

            return false;
        }
    }
}
