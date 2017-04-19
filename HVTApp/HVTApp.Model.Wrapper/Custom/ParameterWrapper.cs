using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ParameterWrapper
    {
        public bool CanBeSelected(IEnumerable<ParameterWrapper> parameters)
        {
            if (!RequiredParentParametersList.Any()) return true;

            foreach (var requiredParentParametersWrapper in RequiredParentParametersList)
                if (requiredParentParametersWrapper.Parameters.All(parameters.Contains)) return true;

            return false;
        }
    }
}
