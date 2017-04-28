using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ParameterWrapper
    {
        public bool CanBeSelected(IEnumerable<ParameterWrapper> parameters)
        {
            //если нет обязательных родительских параметров
            if (!RequiredParentParametersList.Any()) return true;

            //если обязательные параметры выбраны
            foreach (var requiredParentParametersWrapper in RequiredParentParametersList)
                if (requiredParentParametersWrapper.Parameters.All(parameters.Contains)) return true;

            return false;
        }
    }
}
